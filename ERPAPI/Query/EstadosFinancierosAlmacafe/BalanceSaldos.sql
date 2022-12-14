USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[BalanceDeSaldos]    Script Date: 25/03/2020 13:12:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[BalanceDeSaldos]
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
	DECLARE @Ingresos AS FLOAT;
	DECLARE @Gastos AS FLOAT;
	DECLARE @IngresosPrev AS FLOAT;
	DECLARE @GastosPrev AS FLOAT;
	DECLARE @IngresosPrevAnio AS FLOAT;
	DECLARE @GastosPrevAnio AS FLOAT;

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

	DROP TABLE IF EXISTS #PartExcl;	
	CREATE TABLE #PartExcl(
		Anio INT,
		JournalEntryId BIGINT
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
	ORDER BY AccountCode DESC;	

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

	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,2656);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,3270);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,1748);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,2015);
	--INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,2170);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,5471);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,5472);
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,5473);
	---------------------
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,2655); 
	---INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,2162); 
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2018,2016); 


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
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = @ANIO AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaIniActual AND Cab.EstadoId = 6
				) Det ON Det.AccountId = Cta.AccountId
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
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted >= @FechaIniActual AND Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinAnioPrev AND Cab.EstadoId = 6 AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = (@ANIO-1) AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
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
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = @ANIO AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaIniActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId 
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
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted >= @FechaIniActual AND Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinAnioPrev AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = (@ANIO-1) AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
		) Datos
		GROUP BY AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado
		ORDER BY AccountCode;	
	END
		OPEN TotalizaBalancePrev_CURSOR;
		FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN

				SELECT @SaldoPrevAnio = ISNULL(SUM(ISNULL(SaldoPrevAnio,0)),0),
					   @SaldoPrev = ISNULL(SUM(ISNULL(SaldoPrev,0)),0),
					   @Debe =  ISNULL(SUM(ISNULL(Debe,0)),0), 
					   @Haber = ISNULL(SUM(ISNULL(Haber,0)),0), 
					   @Saldo = ISNULL(SUM(ISNULL(SaldoFinal,0)),0)
				FROM #BalanceSaldo
				WHERE ParentAccountId = @AccountId

				UPDATE #BalanceSaldo SET SaldoPrevAnio = SaldoPrevAnio + @SaldoPrevAnio, SaldoPrev = SaldoPrev + @SaldoPrev, Debe = Debe + @Debe, Haber = Haber + @Haber, SaldoFinal = SaldoFinal + @Saldo
				WHERE AccountId = @AccountId
				
				FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalancePrev_CURSOR;
	--Utilidades o Perdidas del Periodo

	SELECT @Ingresos = SaldoFinal, @IngresosPrev = SaldoPrev, @IngresosPrevAnio = SaldoPrevAnio
	FROM #BalanceSaldo
	WHERE AccountCode = '5'

	SELECT @Gastos = SaldoFinal, @GastosPrev = SaldoPrev, @GastosPrevAnio = SaldoPrevAnio
	FROM #BalanceSaldo
	WHERE AccountCode = '6'

	TRUNCATE TABLE #BalanceSaldo

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
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = @ANIO AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaIniActual AND Cab.EstadoId = 6
				) Det ON Det.AccountId = Cta.AccountId
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
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted >= @FechaIniActual AND Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinAnioPrev AND Cab.EstadoId = 6 AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = (@ANIO-1) AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
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
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = @ANIO AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					0 SaldoPrevAnio,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaIniActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId 
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
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted >= @FechaIniActual AND Cab.DatePosted < @FechaFinActual AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
			UNION ALL
			SELECT Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado,
					CASE WHEN DeudoraAcreedora = 'D' THEN  SUM(ISNULL(Det.Debit,0) - ISNULL(Det.Credit,0))
					ELSE SUM(ISNULL(Det.Credit,0) - ISNULL(Det.Debit,0)) END SaldoPrevAnio,
					0 SaldoPrev,
					0 Debe,
					0 Haber,
					0 SaldoFinal
			FROM Accounting Cta
			LEFT JOIN(
				SELECT Cab.DatePosted, Det.AccountId, Det.Credit, Det.Debit 
				FROM JournalEntryLine Det
				JOIN JournalEntry Cab ON Cab.JournalEntryId = Det.JournalEntryId
				WHERE Cab.DatePosted < @FechaFinAnioPrev AND Cab.EstadoId = 6 AND Det.CostCenterId = @CENTROCOSTO AND NOT EXISTS(SELECT 1 FROM #PartExcl E WHERE E.Anio = (@ANIO-1) AND Cab.JournalEntryId = E.JournalEntryId)
				) Det ON Det.AccountId = Cta.AccountId
			GROUP BY Cta.AccountId, Cta.AccountCode, Cta.HierarchyAccount, Cta.AccountName, Cta.ParentAccountId, Cta.Totaliza, Cta.DeudoraAcreedora, Cta.Estado
		) Datos
		GROUP BY AccountId, AccountCode, HierarchyAccount, AccountName, ParentAccountId, Totaliza, DeudoraAcreedora, Estado
		ORDER BY AccountCode;	
	END

	IF @Gastos > @Ingresos
		UPDATE #BalanceSaldo SET SaldoFinal = @Ingresos - @Gastos WHERE AccountCode = '32502';
	ELSE	
		UPDATE #BalanceSaldo SET SaldoFinal = @Ingresos - @Gastos WHERE AccountCode = '32501';		

	IF @GastosPrev > @IngresosPrev
		UPDATE #BalanceSaldo SET SaldoPrev = @IngresosPrev - @GastosPrev WHERE AccountCode = '32502';
	ELSE	
		UPDATE #BalanceSaldo SET SaldoPrev = @IngresosPrev - @GastosPrev WHERE AccountCode = '32501';		

	IF @GastosPrevAnio > @IngresosPrevAnio
		UPDATE #BalanceSaldo SET SaldoPrevAnio = @IngresosPrevAnio - @GastosPrevAnio WHERE AccountCode = '32502';
	ELSE	
		UPDATE #BalanceSaldo SET SaldoPrevAnio = @IngresosPrevAnio - @GastosPrevAnio WHERE AccountCode = '32501';		
	
	OPEN TotalizaBalancePrev_CURSOR;
		FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				SELECT @SaldoPrevAnio = ISNULL(SUM(ISNULL(SaldoPrevAnio,0)),0),
					   @SaldoPrev = ISNULL(SUM(ISNULL(SaldoPrev,0)),0),
					   @Debe =  ISNULL(SUM(ISNULL(Debe,0)),0), 
					   @Haber = ISNULL(SUM(ISNULL(Haber,0)),0), 
					   @Saldo = ISNULL(SUM(ISNULL(SaldoFinal,0)),0)
				FROM #BalanceSaldo
				WHERE ParentAccountId = @AccountId

				UPDATE #BalanceSaldo SET SaldoPrevAnio = SaldoPrevAnio + @SaldoPrevAnio, SaldoPrev = SaldoPrev + @SaldoPrev, Debe = Debe + @Debe, Haber = Haber + @Haber, SaldoFinal = SaldoFinal + @Saldo
				WHERE AccountId = @AccountId
				
				FETCH NEXT FROM TotalizaBalancePrev_CURSOR INTO @AccountId;
			END;
		CLOSE TotalizaBalancePrev_CURSOR;
		DEALLOCATE TotalizaBalancePrev_CURSOR;

	IF @Gastos < @Ingresos
	BEGIN
		UPDATE #BalanceSaldo SET SaldoFinal = @Ingresos - @Gastos WHERE AccountCode = '581';		
		UPDATE #BalanceSaldo SET SaldoFinal = SaldoFinal + (@Ingresos - @Gastos) WHERE AccountCode = '58';		
	END;
	IF @GastosPrev < @IngresosPrev
	BEGIN
		UPDATE #BalanceSaldo SET SaldoPrev = @IngresosPrev - @GastosPrev WHERE AccountCode = '581';		
		UPDATE #BalanceSaldo SET SaldoPrev = SaldoPrev + (@IngresosPrev - @GastosPrev) WHERE AccountCode = '58';		
	END;
	IF @GastosPrevAnio < @IngresosPrevAnio
	BEGIN
		UPDATE #BalanceSaldo SET SaldoPrevAnio = @IngresosPrevAnio - @GastosPrevAnio WHERE AccountCode = '581';		
		UPDATE #BalanceSaldo SET SaldoPrevAnio = SaldoPrevAnio + (@IngresosPrevAnio - @GastosPrevAnio) WHERE AccountCode = '58';		
	END;
	

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
