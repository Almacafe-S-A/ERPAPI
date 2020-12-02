USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[ActualizarSaldoCatalogoContable]    Script Date: 29/1/2020 10:49:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[ActualizarSaldoCatalogoContable]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	 
declare @parentid bigint;
declare @accountid bigint;
declare @credit float;
declare @debit float;
declare @journalentryid bigint;

BEGIN TRY
	BEGIN TRAN

	CREATE TABLE  #CuentasAActualizar(id int primary key identity(1,1), AccountId bigint, AcreedoraDeudora NVARCHAR(1), accountBalance float, journalentryid bigint)

	DECLARE cursor_product CURSOR
	FOR SELECT T1.AccountId,
		T1.Credit,
		T1.Debit,
		T0.JournalEntryId
		FROM JournalEntry T0
		INNER JOIN JournalEntryLine T1 ON T0.JournalEntryId = T1.JournalEntryId
		WHERE YEAR(T0.Date) < 2020;

	OPEN cursor_product;

	FETCH NEXT FROM cursor_product INTO 
		@accountid, 
		@credit,
		@debit,
		@journalentryid;
 
	WHILE @@FETCH_STATUS = 0
		BEGIN

		UPDATE Accounting
		SET AccountBalance = 
				CASE DeudoraAcreedora
					WHEN 'D' THEN 
						(AccountBalance - @credit + @debit)
					WHEN 'A' THEN
						(AccountBalance + @credit - @debit)
				END
		WHERE AccountId = @accountid

		insert into #CuentasAActualizar(AccountId, AcreedoraDeudora, accountBalance, journalentryid) 
		(select AccountId, DeudoraAcreedora, AccountBalance, @journalentryid
		FROM Accounting WHERE AccountId = @accountid)

		SET @parentid = (select ParentAccountId from Accounting WHERE AccountId = @accountid)

		UPDATE JournalEntry 
		SET EstadoId = 6, IdEstado = 6, Estado = 'Aprobado', EstadoName = 'Aprobado'
		WHERE JournalEntryId = @journalentryid

WHILE @parentid is not null
BEGIN

		UPDATE Accounting
		SET AccountBalance = 
				CASE DeudoraAcreedora
					WHEN 'D' THEN 
						(AccountBalance - @credit + @debit)
					WHEN 'A' THEN
						(AccountBalance + @credit - @debit)
				END
		WHERE AccountId = @parentid

insert into #CuentasAActualizar(AccountId, AcreedoraDeudora, accountBalance, journalentryid) 
(select AccountId, DeudoraAcreedora, AccountBalance, @journalentryid
FROM Accounting WHERE AccountId = @parentid)

SET @parentid = (select ParentAccountId from Accounting WHERE AccountId = @parentid)
END

        FETCH NEXT FROM cursor_product INTO 
			@accountid, 
			@credit,
			@debit,
			@journalentryid;
    END;

	CLOSE cursor_product;
DEALLOCATE cursor_product;

SELECT * FROM #CuentasAActualizar

DROP TABLE #CuentasAActualizar

	COMMIT TRAN
END TRY
BEGIN CATCH
    IF(@@TRANCOUNT > 0)
        ROLLBACK TRAN;

	THROW;
END CATCH
	 
END

GO


