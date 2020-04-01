USE [ERP-PROD]
GO
/****** Object:  StoredProcedure [dbo].[GenerarBalanceComparativo2]    Script Date: 20/03/2020 15:25:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER   PROCEDURE [dbo].[GenerarBalanceComparativo2]
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
		AccountName NVARCHAR(MAX),
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

	DROP TABLE IF EXISTS #BalanceComparativo;	

	CREATE TABLE #BalanceComparativo (
		Nota INT,
		AccountId INT,
		AccountCode NVARCHAR(50),
		Descripcion NVARCHAR(MAX),
		ParentAccountId INT,
		DeudoraAcreedora NVARCHAR(1),
		Estado NVARCHAR(MAX),
		Totaliza BIT,
		AñoAnterior FLOAT,
		SaldoPrev FLOAT,
		Debe FLOAT,
		Haber FLOAT,
		AñoActual FLOAT,
		TypeAccountId INT,
		TipoDeCuenta NVARCHAR(50),
		SubCuentaId INT,
		SubCuenta NVARCHAR(MAX),
		Nivel INT,
		Columna INT
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
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,2655); 
	INSERT INTO #PartExcl (Anio, JournalEntryId) VALUES (2019,2162);

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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)				
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6) 
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6) 
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (5,6)
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




	INSERT INTO #BalanceComparativo
	SELECT 
	Nota = CASE WHEN B.HierarchyAccount != 2 THEN NULL ELSE ROW_NUMBER() OVER (PARTITION BY CASE WHEN B.HierarchyAccount != 2 THEN 1 ELSE 0 END ORDER BY B.AccountId) END + 3,
	B.AccountId, B.AccountCode, B.AccountName AS 'Descripcion', B.ParentAccountId, 
	CASE
	WHEN B.DeudoraAcreedora = 'A' THEN '2'
	ELSE '1'
	END AS 'DeudoraAcreedora', B.Estado, B.Totaliza, 
	--CASE T2.AceptaNegativo
	--WHEN 1 THEN
	--	Round(B.SaldoPrevAnio,2) * -1
	--ELSE
	--	Round(B.SaldoPrevAnio,2)
	--END
	Round(B.SaldoPrevAnio,2) AS 'AñoAnterior', 
	Round(B.SaldoPrev,2) SaldoPrev, Round(B.Debe,2) Debe, Round(B.Haber,2) Haber, 
	--CASE T2.AceptaNegativo
	--WHEN 1 THEN
	--	Round(B.SaldoFinal,2) * -1
	--ELSE
	--	Round(B.SaldoFinal,2)
	--END
	Round(B.SaldoFinal,2) AS 'AñoActual'
	, T2.TypeAccountId, T3.TypeAccountName AS 'TipoDeCuenta'
	, T4.ParentAccountId AS 'SubCuentaId'
	, T5.Description AS 'SubCuenta'
	, B.HierarchyAccount AS 'Nivel'
	, ROW_NUMBER() OVER (PARTITION BY B.DeudoraAcreedora ORDER BY B.AccountCode) AS 'Columna'
	FROM #BalanceSaldo B	
	LEFT JOIN Accounting T2 ON B.AccountId = T2.AccountId
	LEFT JOIN TypeAccount T3 ON T2.TypeAccountId = T3.TypeAccountId
	LEFT JOIN Accounting T4 ON T4.AccountId = T2.ParentAccountId
	LEFT JOIN Accounting T5 ON T4.ParentAccountId = T5.AccountId
	WHERE B.HierarchyAccount <= @NIVEL
	AND NOT (Round(B.SaldoFinal,2) = 0 AND Round(B.SaldoPrevAnio,2) = 0 AND B.HierarchyAccount > 2)
	AND NOT (B.HierarchyAccount = 1 AND T2.TypeAccountId = 4)
	AND NOT (B.HierarchyAccount > 2 AND T2.TypeAccountId = 4)
	ORDER BY B.AccountCode

	DELETE FROM #BalanceComparativo WHERE AccountCode = '58' OR AccountCode = '68'
	DELETE FROM #BalanceComparativo WHERE Nivel = 1

	---REINICIAR EL CONTEO DE COLUMNAS
	DECLARE @id INT 
	SET @id = 0;
	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'TOTAL INGRESOS FINANCIEROS', NULL, '2', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '51'), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '52'), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '53'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '51'), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '52'), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '53'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '51'), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '52'), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '53'), 0)
	, 5, 'TOTAL', NULL, NULL, 1, (SELECT Columna FROM #BalanceComparativo WHERE AccountCode = '54'))

	UPDATE #BalanceComparativo
	SET Columna = Columna + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) > = 54)

	SET @id = (SELECT Columna FROM #BalanceComparativo WHERE AccountCode = '54') - 1
	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 61 AND 63)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'TOTAL GASTOS FINANCIEROS', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '61'), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '62'), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '63'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '61'), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '62'), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '63'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '61'), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '62'), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '63'), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'UTILIDAD O PERDIDA FINANCIERA', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'TOTAL INGRESOS FINANCIEROS' AND Nivel = 1), 0) - COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'TOTAL GASTOS FINANCIEROS' AND Nivel = 1), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'TOTAL INGRESOS FINANCIEROS' AND Nivel = 1), 0) - COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'TOTAL GASTOS FINANCIEROS' AND Nivel = 1), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'TOTAL INGRESOS FINANCIEROS' AND Nivel = 1), 0) - COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'TOTAL GASTOS FINANCIEROS' AND Nivel = 1), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	--SET @id = @id + 1
	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 54 AND 54)

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 65 AND 65)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'UTILIDAD O PERDIDA POR VALORACION DE OTROS ACTIVOS Y PASIVOS', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '54'), 0) - COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '65'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '54'), 0) - COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '65'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '54'), 0) - COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '65'), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 55 AND 55)

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 59 AND 59)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'TOTAL DE INGRESOS', NULL, '2', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '55'), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '59'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '55'), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '59'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '55'), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '59'), 0)
	, 5, 'TOTAL', NULL, NULL, 1, @id)

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 64 AND 64)

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 69 AND 69)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'TOTAL DE GASTOS', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '64'), 0) - COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '69'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '64'), 0) - COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '69'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '64'), 0) - COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '69'), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)
	
	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'UTILIDAD DE OPERACION', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE INGRESOS' AND Nivel = 1), 0) - COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE GASTOS' AND Nivel = 1), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE INGRESOS' AND Nivel = 1), 0) - COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE GASTOS' AND Nivel = 1), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE INGRESOS' AND Nivel = 1), 0) - COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'TOTAL DE GASTOS' AND Nivel = 1), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, NULL, 'UTILIDAD ANTES DEL ISR', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD DE OPERACION' AND Nivel = 1), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA POR VALORACION DE OTROS ACTIVOS Y PASIVOS' AND Nivel = 1), 0) + COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA FINANCIERA' AND Nivel = 1), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD DE OPERACION' AND Nivel = 1), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA POR VALORACION DE OTROS ACTIVOS Y PASIVOS' AND Nivel = 1), 0) + COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA FINANCIERA' AND Nivel = 1), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD DE OPERACION' AND Nivel = 1), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA POR VALORACION DE OTROS ACTIVOS Y PASIVOS' AND Nivel = 1), 0) + COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD O PERDIDA FINANCIERA' AND Nivel = 1), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	DELETE FROM #BalanceComparativo
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 66 AND 66) AND NIVEL > 2

	UPDATE #BalanceComparativo
	SET @id = Columna = @id + 1
	WHERE (CAST(SUBSTRING(AccountCode, 1, 2) AS BIGINT) BETWEEN 66 AND 66)

	SET @id = @id + 1
	INSERT INTO #BalanceComparativo 
	VALUES (NULL, NULL, '58/68', 'UTILIDAD O PERDIDA NETA', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD ANTES DEL ISR' AND Nivel = 1), 0) - COALESCE((SELECT AñoAnterior FROM #BalanceComparativo WHERE AccountCode = '66'), 0),
	COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD ANTES DEL ISR' AND Nivel = 1), 0) - COALESCE((SELECT SaldoPrev FROM #BalanceComparativo WHERE AccountCode = '66'), 0)
	, 0, 0,
	COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE Descripcion = 'UTILIDAD ANTES DEL ISR' AND Nivel = 1), 0) - COALESCE((SELECT AñoActual FROM #BalanceComparativo WHERE AccountCode = '66'), 0)
	, 6, 'TOTAL', NULL, NULL, 1, @id)

	SELECT * FROM #BalanceComparativo T0
	ORDER BY T0.Columna
END											
