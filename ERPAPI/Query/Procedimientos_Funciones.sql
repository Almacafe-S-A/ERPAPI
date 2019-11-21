CREATE FUNCTION [dbo].[SumaCredito]
(  
	 @FechaInicio DATETIME,
   @FechaFin DATETIME,
	 @cuenta INT 
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Credit)       
 FROM JournalEntryLine  T1 
   INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where t1.AccountId = @cuenta
    AND je.Date>=@FechaInicio AND je.Date<=@FechaFin  )

  return @sumatipoisv;
END

GO


CREATE FUNCTION [dbo].[SumaDebito]
(  
   @FechaInicio DATETIME,
   @FechaFin DATETIME,
	 @cuenta int
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Debit)       
 FROM JournalEntryLine  T1 
    INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where t1.AccountId = @cuenta
   AND je.Date>=@FechaInicio AND je.Date<=@FechaFin
  )

  return @sumatipoisv;
END

GO



CREATE FUNCTION [dbo].[TotalDebito]
(  
   @FechaInicio DATETIME,
   @FechaFin DATETIME
	
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Debit)       
 FROM JournalEntryLine  T1 
    INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where  je.Date>=@FechaInicio AND je.Date<=@FechaFin
  )

  return @sumatipoisv;
END

GO

CREATE FUNCTION [dbo].[TotalCredito]
(  
	 @FechaInicio DATETIME,
   @FechaFin DATETIME
	 
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Credit)       
 FROM JournalEntryLine  T1 
   INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where 
     je.Date>=@FechaInicio AND je.Date<=@FechaFin  )

  return @sumatipoisv;
END

GO



SELECT    COALESCE(dbo.[TotalCredito]('2019-10-01','2019-10-01'),0) as TotalCredit , COALESCE(dbo.[TotalDebito]('2019-10-01','2019-10-01'),0) as TotalDebit
  , COALESCE(dbo.[TotalDebito]('2019-10-01','2019-10-01'),0) -   COALESCE(dbo.[TotalCredito]('2019-10-01','2019-10-01'),0) AccountBalance      




  ------------------------------------------Cierre Contable-------------------------------------
 
/****** Object:  StoredProcedure [dbo].[Cierres]    Script Date: 21/11/2019 8:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[Cierres] 
	--Parametros
	@pFechaInicio	DateTime,
	@pIdBitacora    int
	
AS

	DECLARE @result int = 0
BEGIN
	--PASO1
	BEGIN TRANSACTION    
		BEGIN TRY  

			--Paso 1
			EXEC @result = [dbo].[CierresPaso1_Historicos]  @pFechaInicio, @pIdBitacora;

			--Paso 2 
			EXEC @result = [dbo].[CierresPaso2_CertificadosMaxSum] @pFechaInicio, @pIdBitacora;

			--Paso 3
			--EXEC @result = [dbo].[CierresPaso3_CertificadosMaxSum] @pFechaInicio, @pIdBitacora;


		END TRY  
		BEGIN CATCH  
			EXECUTE usp_GetErrorInfo;  
			IF @@TRANCOUNT > 0  
				ROLLBACK TRANSACTION; 				
		END CATCH;    
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  	
	return @result;
END 


GO
/****** Object:  StoredProcedure [dbo].[CierresPaso1_Historicos]    Script Date: 21/11/2019 8:32:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CierresPaso1_Historicos] 
	
	@pFechaInicio	DateTime,
	@pIdBitacora    int
AS
BEGIN
			INSERT INTO [dbo].[CierresAccounting]
           ([AccountId]
           ,[ParentAccountId]
           ,[CompanyInfoId]
           ,[AccountBalance]
           ,[Description]
           ,[IsCash]
           ,[AccountClasses]
           ,[IsContraAccount]
           ,[TypeAccountId]
           ,[BlockedInJournal]
           ,[AccountCode]
           ,[IdEstado]
           ,[Estado]
           ,[HierarchyAccount]
           ,[AccountName]
           ,[UsuarioCreacion]
           ,[UsuarioModificacion]
           ,[FechaCreacion]
           ,[FechaModificacion]
           ,[ParentAccountAccountId]
		   ,[BitacoraCierreContableId]
           ,[FechaCierre])
     
			SELECT [AccountId]
           ,[ParentAccountId]
           ,[CompanyInfoId]
           ,[AccountBalance]
           ,[Description]
           ,[IsCash]
           ,[AccountClasses]
           ,[IsContraAccount]
           ,[TypeAccountId]
           ,[BlockedInJournal]
           ,[AccountCode]
           ,[IdEstado]
           ,[Estado]
           ,[HierarchyAccount]
           ,[AccountName]
           ,[UsuarioCreacion]
           ,[UsuarioModificacion]
           ,[FechaCreacion]
           ,[FechaModificacion]
           ,[ParentAccountAccountId]
		   ,@pIdBitacora
           ,GETDATE() FROM Accounting	;	

		   --INSERTA LOS DIARIOS A LOS HISTORICOS

	INSERT INTO CierresJournal
					   ([FechaCierre]
					   ,[GeneralLedgerHeaderId]
					   ,[PartyTypeId]
					   ,[PartyTypeName]
					   ,[DocumentId]
					   ,[PartyId]
					   ,[VoucherType]
					   ,[TypeJournalName]
					   ,[Date]
					   ,[DatePosted]
					   ,[Memo]
					   ,[ReferenceNo]
					   ,[Posted]
					   ,[GeneralLedgerHeaderId1]
					   ,[PartyId1]
					   ,[IdPaymentCode]
					   ,[IdTypeofPayment]
					   ,[EstadoId]
					   ,[EstadoName]
					   ,[TotalDebit]
					   ,[TotalCredit]
					   ,[TypeOfAdjustmentId]
					   ,[TypeOfAdjustmentName]
					   ,[CreatedUser]
					   ,[ModifiedUser]
					   ,[CreatedDate]
					   ,[ModifiedDate]
					   ,[BitacoraCierreContableId]
					   ,[JournalEntryId])

				SELECT SYSDATETIME()
					  ,[GeneralLedgerHeaderId]
					  ,[PartyTypeId]
					  ,[PartyTypeName]
					  ,[DocumentId]
					  ,[PartyId]
					  ,[VoucherType]
					  ,[TypeJournalName]
					  ,[Date]
					  ,[DatePosted]
					  ,[Memo]
					  ,[ReferenceNo]
					  ,[Posted]
					  ,[GeneralLedgerHeaderId1]
					  ,[PartyId1]
					  ,[IdPaymentCode]
					  ,[IdTypeofPayment]
				      ,[EstadoId]
					  ,[EstadoName]
					  ,[TotalDebit]
					  ,[TotalCredit]
					  ,[TypeOfAdjustmentId]
					  ,[TypeOfAdjustmentName]
					  ,[CreatedUser]
					  ,[ModifiedUser]
					  ,[CreatedDate]
					  ,[ModifiedDate]
					  ,@pIdBitacora
					  ,[JournalEntryId]
				  FROM JournalEntry;
				
				-- INSERT LOS JOURNAL ENTRY LINES

				PRINT 'INSERT LOS JOURNAL ENTRY LINES'

			INSERT INTO CierresJournalEntryLine
					   ([JournalEntryLineId]
					   ,[JournalEntryId]
					   ,[Description]
					   ,[AccountId]
					   ,[DebitSy]
					   ,[Memo]
					   ,[AccountId1]
					   ,[CreatedUser]
					   ,[ModifiedUser]
					   ,[CreatedDate]
					   ,[ModifiedDate]
					   ,[Credit]
					   ,[CreditME]
					   ,[CreditSy]
					   ,[Debit]
					   ,[DebitME]
					   ,[CostCenterId]
					   ,[CostCenterName]
					   ,[AccountName]
					   )

				SELECT [JournalEntryLineId]
					  ,[JournalEntryId]
					  ,[Description]
					  ,[AccountId]
					  ,[DebitSy]
					  ,[Memo]
					  ,[AccountId1]
					  ,[CreatedUser]
					  ,[ModifiedUser]
					  ,[CreatedDate]
					  ,[ModifiedDate]
					  ,[Credit]
					  ,[CreditME]
					  ,[CreditSy]
					  ,[Debit]
					  ,[DebitME]
					  ,[CostCenterId]
					  ,[CostCenterName]
					  ,[AccountName]
				  FROM [dbo].[JournalEntryLine]

			PRINT 'INSERTO EN LA BITACORA DE PROCESOS';
END
GO
/****** Object:  StoredProcedure [dbo].[CierresPaso2_CertificadosMaxSum]    Script Date: 21/11/2019 8:32:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CierresPaso2_CertificadosMaxSum]
	-- Parametros
	@pFechaInicio	DateTime,
	@pIdBitacora    int
AS
BEGIN
	DECLARE @ValorAcumulado decimal;
	SET NOCOUNT ON;

	Select  @ValorAcumulado = SUM(cd.Total) from CertificadoDeposito cd

	UPDATE ElementoConfiguracion SET Valordecimal = @ValorAcumulado Where ElementoConfiguracion.Nombre = 'VALOR MAXIMO CERTIFICADOS'

   
END
GO
