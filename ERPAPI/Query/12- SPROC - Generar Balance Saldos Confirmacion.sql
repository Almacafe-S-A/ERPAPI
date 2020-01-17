USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[GenerarBanlance]    Script Date: 1/12/2019 9:15:06 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS [dbo].[BalanceDeSaldos]
GO

CREATE PROCEDURE [dbo].[BalanceDeSaldos]
    @MES INT,
	@ANIO INT,	
	@NIVEL INT,
	@CENTROCOSTO BIGINT
AS
BEGIN		
	DECLARE @FechaIniActual AS DATE;
	DECLARE @FechaIniComparativo AS DATE;
	DECLARE @FechaFinActual AS DATE;
	DECLARE @FechaFinComparativo AS DATE;
	DECLARE @FechaFinAnioPrev AS DATE;
	DECLARE @AccountId AS BIGINT;
	DECLARE @SaldoPrevAnio AS FLOAT;
	DECLARE @SaldoPrev AS FLOAT;
	DECLARE @Debe AS FLOAT;
	DECLARE @Haber AS FLOAT;
	DECLARE @Saldo AS FLOAT;	

	DROP TABLE IF EXISTS #BalanceSaldo;	
	
	CREATE TABLE #BalanceSaldo (
		AccountId INT,
		AccountCode NVARCHAR(50),
		HierarchyAccount BIGINT,
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		Totaliza BIT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		SaldoPrevAnio FLOAT,
		SaldoPrev FLOAT,
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT
	);

	DECLARE TotalizaBalance_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #BalanceSaldo
	WHERE Totaliza = 1
	ORDER BY AccountCode DESC;	

	DECLARE TotalizaBalancePrev_CURSOR CURSOR FOR
	SELECT AccountId
	FROM #BalanceSaldo
	WHERE Totaliza = 1
	ORDER BY AccountCode;	

	IF @MES = 12
		BEGIN
			SET @FechaFinActual = STR(@ANIO+1) + '-01-01';
			SET @FechaFinAnioPrev = STR(@ANIO) + '-01-01';
			SET @FechaFinComparativo = STR(@ANIO) + '-01-01';
		END
	ELSE
		BEGIN
			SET @FechaFinActual = STR(@ANIO) + '-' + RIGHT('00'+STR(@MES+1),2) + '-01';
			SET @FechaFinAnioPrev = STR(@ANIO-1) + '-' + RIGHT('00'+STR(@MES+1),2) + '-01';
			SET @FechaFinComparativo = STR(@ANIO-1) + '-' + RIGHT('00'+STR(@MES+1),2) + '-01';
		END
	SET @FechaIniActual = STR(@ANIO) + '-' + RIGHT('00'+STR(@MES),2) + '-01';
	SET @FechaIniComparativo = STR(@ANIO) + '-01-01';

	IF @CENTROCOSTO = 0 
	BEGIN
		INSERT INTO #BalanceSaldo
		SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado,SUM(SaldoPrevAnio) SaldoPrevAnio, SUM(SaldoPrev) SaldoPrev, SUM(Debe) Debe, SUM(Haber) Haber, SUM(SaldoFinal) SaldoFinal
		FROM
		(
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaFinActual
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoPrev,
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaIniActual
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					0 SaldoPrev,
					SUM(ISNULL(Det.Debit,0)) Debe,
					SUM(ISNULL(Det.Credit,0)) Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoPrevAnio,
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaFinAnioPrev
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
		) Datos
		GROUP BY AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado
		ORDER BY AccountCode;
	END
	ELSE
	BEGIN
		INSERT INTO #BalanceSaldo
		SELECT AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado, SUM(SaldoPrevAnio), SUM(SaldoPrev) SaldoPrev, SUM(Debe) Debe, SUM(Haber) Haber, SUM(SaldoFinal) SaldoFinal
		FROM
		(
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoFinal
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaFinActual AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoPrev,
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaIniActual AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId 
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					0 SaldoPrev,
					SUM(ISNULL(Det.Debit,0)) Debe,
					SUM(ISNULL(Det.Credit,0)) Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date >= @FechaIniActual AND Cab.Date < @FechaFinActual AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					ABS(SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))) SaldoPrevAnio,
					--SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0)) SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.Date, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.Date < @FechaFinAnioPrev AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId
			WHERE Cta.AccountCode NOT LIKE '7%'
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
		) Datos
		GROUP BY AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado
		ORDER BY AccountCode;	
	END

	IF @ANIO IN (2017,2018,2019)
	BEGIN
		OPEN TotalizaBalancePrev_CURSOR;
		FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, SaldoPrevAnio, SaldoPrev, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceSaldo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode,T.SaldoPrevAnio, T.SaldoPrev, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceSaldo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @SaldoPrevAnio = ISNULL(SUM(ISNULL(SaldoPrevAnio,0)),0),
					   @SaldoPrev = ISNULL(SUM(ISNULL(SaldoPrev,0)),0),
					   @Debe =  ISNULL(SUM(ISNULL(Debe,0)),0), 
					   @Haber = ISNULL(SUM(ISNULL(Haber,0)),0), 
					   @Saldo = ISNULL(SUM(ISNULL(SaldoFinal,0)),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				
				UPDATE #BalanceSaldo SET SaldoPrevAnio = SaldoPrevAnio + @SaldoPrevAnio, SaldoPrev = SaldoPrev + @SaldoPrev, Debe = Debe + @Debe, Haber = Haber + @Haber, SaldoFinal = SaldoFinal + @Saldo
				WHERE AccountId = @AccountId				
				
				FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalancePrev_CURSOR;
		DEALLOCATE TotalizaBalancePrev_CURSOR;
	END
	ELSE IF @ANIO = 2020
	BEGIN
		OPEN TotalizaBalancePrev_CURSOR;
		FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, SaldoPrevAnio, SaldoPrev, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceSaldo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode,T.SaldoPrevAnio, T.SaldoPrev, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceSaldo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT @SaldoPrevAnio = ISNULL(SUM(ISNULL(SaldoPrevAnio,0)),0)					   
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				
				UPDATE #BalanceSaldo SET SaldoPrevAnio = SaldoPrevAnio + @SaldoPrevAnio
				WHERE AccountId = @AccountId				
				
				FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalancePrev_CURSOR;
		DEALLOCATE TotalizaBalancePrev_CURSOR;
		OPEN TotalizaBalance_CURSOR;
		FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, SaldoPrev, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceSaldo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.SaldoPrev, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceSaldo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT	@SaldoPrev =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoPrev,0) END),0), 
						@Debe =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Debe,0) END),0), 
						@Haber = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Haber,0) END),0), 
						@Saldo = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoFinal,0) END),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				UPDATE #BalanceSaldo SET SaldoPrev = @SaldoPrev, Debe = @Debe, Haber = @Haber, SaldoFinal = @Saldo
				WHERE AccountId = @AccountId				
				FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalance_CURSOR;
		DEALLOCATE TotalizaBalance_CURSOR;
	END
	ELSE
	BEGIN
		OPEN TotalizaBalance_CURSOR;
		FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				WITH tblCuentas AS
				(
					SELECT AccountId, AccountCode, SaldoPrevAnio, SaldoPrev, Debe, Haber, SaldoFinal, Totaliza
					FROM #BalanceSaldo
					WHERE ParentAccountId = @AccountId
					UNION ALL
					SELECT T.AccountId, T.AccountCode, T.SaldoPrevAnio, T.SaldoPrev, T.Debe, T.Haber, T.SaldoFinal, T.Totaliza
					FROM #BalanceSaldo T  
					JOIN tblCuentas ON T.ParentAccountId = tblCuentas.AccountId					
				)
				SELECT	@SaldoPrevAnio = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoPrevAnio,0) END),0),
						@SaldoPrev =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoPrev,0) END),0), 
						@Debe =  ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Debe,0) END),0), 
						@Haber = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(Haber,0) END),0), 
						@Saldo = ISNULL(SUM(CASE WHEN Totaliza=1 THEN 0 ELSE ISNULL(SaldoFinal,0) END),0)
				FROM tblCuentas					
				OPTION(MAXRECURSION 32767)
				UPDATE #BalanceSaldo SET SaldoPrevAnio = @SaldoPrevAnio, SaldoPrev = @SaldoPrev, Debe = @Debe, Haber = @Haber, SaldoFinal = @Saldo
				WHERE AccountId = @AccountId				
				FETCH NEXT FROM TotalizaBalance_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalance_CURSOR;
		DEALLOCATE TotalizaBalance_CURSOR;
	END

	INSERT INTO #BalanceSaldo
	SELECT 999999,'999999',1,'TOTAL',NULL,NULL,NULL,NULL,SUM(ISNULL(SaldoPrevAnio,0)) SaldoPrevAnio, SUM(ISNULL(SaldoPrev,0)) SaldoPrev,
		   SUM(ISNULL(Debe,0)) Debe, SUM(ISNULL(Haber,0)) Haber, SUM(ISNULL(SaldoFinal,0)) SaldoFinal
	FROM #BalanceSaldo
	WHERE ParentAccountId IS NULL;

	SELECT B.AccountId, B.AccountCode, B.AccountName, B.ParentAccountId, B.DeudoraAcreedora, B.Estado, B.Totaliza, Round(B.SaldoPrevAnio,2) SaldoPrevAnio, 
	Round(B.SaldoPrev,2) SaldoPrev, Round(B.Debe,2) Debe, Round(B.Haber,2) Haber, Round(B.SaldoFinal,2) SaldoFinal
	FROM #BalanceSaldo B	
	WHERE B.HierarchyAccount <= @NIVEL
	ORDER BY B.AccountCode
END											
GO
