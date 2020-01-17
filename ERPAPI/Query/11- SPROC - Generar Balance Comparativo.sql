USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[GenerarBanlance]    Script Date: 1/12/2019 9:15:06 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS [dbo].[GenerarBalanceComparativo]
GO

CREATE PROCEDURE [dbo].[GenerarBalanceComparativo]
    @MES INT,
	@ANIO INT,
	@COMPANUAL INT,
	@NIVEL INT,
	@CENTROCOSTO BIGINT
AS
BEGIN		
	DECLARE @FechaIniActual AS DATE;
	DECLARE @FechaIniComparativo AS DATE;
	DECLARE @FechaFinActual AS DATE;
	DECLARE @FechaFinComparativo AS DATE;
	DECLARE @AccountId AS BIGINT;
	DECLARE @Debe AS FLOAT;
	DECLARE @Haber AS FLOAT;
	DECLARE @Saldo AS FLOAT;	

	DROP TABLE IF EXISTS #Balance;
	DROP TABLE IF EXISTS #BalanceComparativo;
	
	CREATE TABLE #BalanceComparativo (
		AccountId INT,
		AccountCode NVARCHAR(50),
		HierarchyAccount BIGINT,
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		Totaliza BIT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT
	);

	CREATE TABLE #Balance (
		AccountId INT,
		AccountCode NVARCHAR(50),
		HierarchyAccount BIGINT,
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		Totaliza BIT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT
	);
	
	DECLARE TotalizaBalanceComparativo_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #BalanceComparativo
	WHERE Totaliza = 1
	ORDER BY AccountCode DESC;

	DECLARE TotalizaBalanceComparativoPrev_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #BalanceComparativo
	WHERE Totaliza = 1
	ORDER BY AccountCode;

	DECLARE TotalizaBalance_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #Balance
	WHERE Totaliza = 1
	ORDER BY AccountCode DESC;	

	DECLARE TotalizaBalancePrev_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #Balance
	WHERE Totaliza = 1
	ORDER BY AccountCode;	



	IF @COMPANUAL = 0 
		BEGIN
			IF @MES = 12
				SET @FechaFinActual = STR(@ANIO+1) + '-01-01';
			ELSE
				SET @FechaFinActual = STR(@ANIO) + '-' + RIGHT('00'+STR((@MES+1)),2) + '-01';
			
			SET @FechaIniActual = STR(@ANIO) + '-' + RIGHT('00'+STR(@MES),2) + '-01';
			SET @FechaIniComparativo = STR(@ANIO) + '-01-01';

			IF @CENTROCOSTO = 0 
			BEGIN
				INSERT INTO #BalanceComparativo
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniComparativo AND Cab.Date < @FechaIniActual
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode

				INSERT INTO #Balance
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode
			END;
			ELSE
			BEGIN
				INSERT INTO #BalanceComparativo
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniComparativo AND Cab.Date < @FechaIniActual AND Det.CostCenterId = @CENTROCOSTO
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode

				INSERT INTO #Balance
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual AND Det.CostCenterId = @CENTROCOSTO
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode
			END;
			
		END
	ELSE
		BEGIN
			IF @MES = 12
				BEGIN
					SET @FechaFinActual = STR(@ANIO+1) + '-01-01';
					SET @FechaFinComparativo = STR(@ANIO) + '-01-01';
				END
			ELSE
				BEGIN
					SET @FechaFinActual = STR(@ANIO) + '-' + RIGHT('00'+STR(@MES+1),2) + '-01';
					SET @FechaFinComparativo = STR(@ANIO-1) + '-' + RIGHT('00'+STR(@MES+1),2) + '-01';
				END
			SET @FechaIniActual = STR(@ANIO) + '-' + RIGHT('00'+STR(@MES),2) + '-01';
			SET @FechaIniComparativo = STR(@ANIO) + '-01-01';

			IF @CENTROCOSTO = 0 
			BEGIN
				INSERT INTO #BalanceComparativo
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniComparativo AND Cab.Date < @FechaFinComparativo
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode;

				INSERT INTO #Balance
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode;
			END;
			ELSE
			BEGIN
				INSERT INTO #BalanceComparativo
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniComparativo AND Cab.Date < @FechaFinComparativo AND Det.CostCenterId = @CENTROCOSTO
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode;

				INSERT INTO #Balance
				SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, Debe, Haber, SaldoFinal			
				FROM
				(
					SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
							SUM(ISNULL(Det.Debit,0)) Debe,
							SUM(ISNULL(Det.Credit,0)) Haber,
							SUM(ABS(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					FROM Accounting Cta
					LEFT JOIN(
						SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
						FROM JournalEntryLine Det
						JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
						WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual AND Det.CostCenterId = @CENTROCOSTO
						) Det ON Det.AccountId = Cta.AccountId
					WHERE Cta.AccountCode NOT LIKE '7%'
					GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
				) Datos
				ORDER BY AccountCode;
			END;
		END

	--EXCEPCION PARA AÑOS MIGRADOS 2017, 2018, 2019, 2020 para años previos
	IF @ANIO IN (2017,2018,2019, 2020)
	BEGIN
		OPEN TotalizaBalanceComparativoPrev_CURSOR;
		FETCH NEXT FROM TotalizaBalanceComparativoPrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceComparativo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceComparativo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @Debe =  ISNULL(SUM(ISNULL(Debe,0)),0), 
					   @Haber = ISNULL(SUM(ISNULL(Haber,0)),0), 
					   @Saldo = ISNULL(SUM(ISNULL(SaldoFinal,0)),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)

				UPDATE #BalanceComparativo SET Debe = Debe + @Debe, Haber = Haber + @Haber, SaldoFinal = SaldoFinal + @Saldo
				WHERE AccountId = @AccountId
				FETCH NEXT FROM TotalizaBalanceComparativoPrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalanceComparativoPrev_CURSOR;
		DEALLOCATE TotalizaBalanceComparativoPrev_CURSOR;
	END;
	ELSE
	BEGIN
		OPEN TotalizaBalanceComparativo_CURSOR;
		FETCH NEXT FROM TotalizaBalanceComparativo_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceComparativo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceComparativo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @Debe =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Debe,0) END),0), 
					   @Haber = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Haber,0) END),0), 
					   @Saldo = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoFinal,0) END),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)

				UPDATE #BalanceComparativo SET Debe = @Debe, Haber = @Haber, SaldoFinal = @Saldo
				WHERE AccountId = @AccountId
				FETCH NEXT FROM TotalizaBalanceComparativo_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalanceComparativo_CURSOR;
		DEALLOCATE TotalizaBalanceComparativo_CURSOR;
	END;

	IF @ANIO IN (2017,2018,2019)
	BEGIN
		OPEN TotalizaBalancePrev_CURSOR;
		FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, Debe, Haber, SaldoFinal, Totaliza
					FROM #Balance
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #Balance T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @Debe =  ISNULL(SUM(ISNULL(Debe,0)),0), 
						@Haber = ISNULL(SUM(ISNULL(Haber,0)),0), 
						@Saldo = ISNULL(SUM(ISNULL(SaldoFinal,0)),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				UPDATE #Balance SET Debe = Debe + @Debe, Haber = Haber + @Haber, SaldoFinal = SaldoFinal + @Saldo
				WHERE AccountId = @AccountId				
				FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalancePrev_CURSOR;
		DEALLOCATE TotalizaBalancePrev_CURSOR;
	END;
	ELSE
	BEGIN
		OPEN TotalizaBalance_CURSOR;
		FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, Debe, Haber, SaldoFinal, Totaliza
					FROM #Balance
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #Balance T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @Debe =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Debe,0) END),0), 
						@Haber = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Haber,0) END),0), 
						@Saldo = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoFinal,0) END),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				UPDATE #Balance SET Debe = @Debe, Haber = @Haber, SaldoFinal = @Saldo
				WHERE AccountId = @AccountId				
				FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalance_CURSOR;
		DEALLOCATE TotalizaBalance_CURSOR;
	END;

	INSERT INTO #BalanceComparativo
	SELECT 999999,'999999',1,'TOTAL',NULL,NULL,NULL,NULL,SUM(ISNULL(Debe,0)) Debe, SUM(ISNULL(Haber,0)) Haber, SUM(ISNULL(SaldoFinal,0)) SaldoFinal
	FROM #BalanceComparativo
	WHERE ParentAccountId IS NULL;
	

	INSERT INTO #Balance
	SELECT 999999,'999999',1,'TOTAL',NULL,NULL,NULL,NULL,SUM(ISNULL(Debe,0)) Debe, SUM(ISNULL(Haber,0)) Haber, SUM(ISNULL(SaldoFinal,0)) SaldoFinal
	FROM #Balance
	WHERE ParentAccountId IS NULL;
	
	
	SELECT B.AccountId, B.AccountCode, B.AccountName, B.ParentAccountId, B.DeudoraAcreedora, B.Estado, B.Totaliza, C.SaldoFinal AS SaldoPrevio, B.Debe, B.Haber, B.SaldoFinal
	FROM #Balance B
	JOIN #BalanceComparativo C ON C.AccountId = B.AccountId
	WHERE B.HierarchyAccount <= @NIVEL
	ORDER BY B.AccountCode
END
											
GO


