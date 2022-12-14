USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[GenerarEstadoSituacionFinanciera2]    Script Date: 25/03/2020 13:10:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   PROCEDURE [dbo].[GenerarEstadoSituacionFinanciera2]
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
	--DECLARE @MES INT;
	DECLARE @Ingresos AS FLOAT;
	DECLARE @Gastos AS FLOAT;
	DECLARE @IngresosPrev AS FLOAT;
	DECLARE @GastosPrev AS FLOAT;
	DECLARE @IngresosPrevAnio AS FLOAT;
	DECLARE @GastosPrevAnio AS FLOAT;


	--SET @MES = 12;

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

	DROP TABLE IF EXISTS #EstadoSituacionFinanciera;	

	CREATE TABLE #EstadoSituacionFinanciera (
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

	SET @FechaFinActual = STR(@ANIO+1) + '-01-01';
	SET @FechaFinAnioPrev = STR(@ANIO) + '-01-01';
	SET @FechaFinComparativo = STR(@ANIO) + '-01-01';

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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4) 
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
				) Det ON Det.AccountId = Cta.AccountId WHERE Cta.TypeAccountId IN (1,2,3,4)
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
	INSERT INTO #EstadoSituacionFinanciera
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


	INSERT INTO #EstadoSituacionFinanciera 
	SELECT Nota, AccountId, AccountCode, 'TOTAL ACTIVO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
	, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, (SELECT MAX(Columna) + 1 FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 1)
	FROM #EstadoSituacionFinanciera WHERE AccountCode = '1'

	UPDATE #EstadoSituacionFinanciera
	SET AñoActual = NULL, AñoAnterior = NULL
	WHERE AccountCode = '1' AND Descripcion != 'TOTAL ACTIVO'

	UPDATE #EstadoSituacionFinanciera
	SET Columna = Columna + 1
	WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1'

	INSERT INTO #EstadoSituacionFinanciera 
	SELECT Nota, AccountId, AccountCode, 'TOTAL PASIVO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
	, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, (SELECT MAX(Columna) + 1 FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 2)
	FROM #EstadoSituacionFinanciera WHERE AccountCode = '2'

	UPDATE #EstadoSituacionFinanciera
	SET AñoActual = NULL, AñoAnterior = NULL
	WHERE AccountCode = '2' AND Descripcion != 'TOTAL PASIVO'

	UPDATE #EstadoSituacionFinanciera
	SET Columna = Columna + 1
	WHERE TypeAccountId = 3 AND DeudoraAcreedora = '2'

	UPDATE #EstadoSituacionFinanciera
	SET Columna = Columna + 1
	WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2'

	INSERT INTO #EstadoSituacionFinanciera 
	SELECT Nota, AccountId, AccountCode, 'TOTAL PATRIMONIO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
	, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, (SELECT MAX(Columna) + 1 FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 3)
	FROM #EstadoSituacionFinanciera WHERE AccountCode = '3'

	UPDATE #EstadoSituacionFinanciera
	SET AñoActual = NULL, AñoAnterior = NULL
	WHERE AccountCode = '3' AND Descripcion != 'TOTAL PATRIMONIO'

	UPDATE #EstadoSituacionFinanciera
	SET Columna = Columna + 1
	WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2'

	INSERT INTO #EstadoSituacionFinanciera 
	SELECT TOP 1 NULL, NULL, NULL, 'TOTAL PASIVO + PATRIMONIO', NULL, '2', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO'), 0) + COALESCE((SELECT AñoAnterior FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PATRIMONIO'), 0)
	AS AñoAnterior,
	COALESCE((SELECT SaldoPrev FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO'), 0) + COALESCE((SELECT SaldoPrev FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PATRIMONIO'), 0)
	AS SaldoPrev,
	0,
	0,
	COALESCE((SELECT AñoActual FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO'), 0) + COALESCE((SELECT AñoActual FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PATRIMONIO'), 0)
	AS AñoActual
	, 3, 'TOTAL', NULL, NULL, 1, (SELECT MIN(Columna) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2')
	FROM #EstadoSituacionFinanciera 

	UPDATE #EstadoSituacionFinanciera
	SET Columna = Columna + 1
	WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2'

	INSERT INTO #EstadoSituacionFinanciera 
	SELECT TOP 1 NULL, NULL, NULL, 'TOTAL ACTIVO + CONTINGENTES', NULL, '1', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL ACTIVO'), 0) + COALESCE((SELECT SUM(AñoAnterior) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1' AND Nivel = 2), 0)
	AS AñoAnterior,
	COALESCE((SELECT SaldoPrev FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL ACTIVO'), 0) + COALESCE((SELECT SUM(SaldoPrev) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1' AND Nivel = 2), 0)
	AS SaldoPrev,
	0,
	0,
	COALESCE((SELECT AñoActual FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL ACTIVO'), 0) + COALESCE((SELECT SUM(AñoActual) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1' AND Nivel = 2), 0)
	AS AñoActual
	, 4, 'TOTAL', NULL, NULL, 1, (SELECT MAX(Columna) + 1 FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1')
	FROM #EstadoSituacionFinanciera

	INSERT INTO #EstadoSituacionFinanciera 
	SELECT TOP 1 NULL, NULL, NULL, 'TOTAL PASIVO + PATRIMONIO + CONTINGENTES', NULL, '2', 'Activo', 1,
	COALESCE((SELECT AñoAnterior FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO'), 0) + COALESCE((SELECT SUM(AñoAnterior) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2' AND Nivel = 2), 0)
	AS AñoAnterior,
	COALESCE((SELECT SaldoPrev FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO'), 0) + COALESCE((SELECT SUM(SaldoPrev) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2' AND Nivel = 2), 0)
	AS SaldoPrev,
	0,
	0,
	COALESCE((SELECT AñoActual FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO'), 0) + COALESCE((SELECT SUM(AñoActual) FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2' AND Nivel = 2), 0)
	AS AñoActual
	, 4, 'TOTAL', NULL, NULL, 1, (SELECT MAX(Columna) + 1 FROM #EstadoSituacionFinanciera WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2')
	FROM #EstadoSituacionFinanciera

	DECLARE @CTotalActivo INT;
	DECLARE @CTotalPasivoPatrimonio INT;
	DECLARE @CTotalActivoContingentes INT;
	DECLARE @CTotalPasivoPatrimonioContingentes INT;

	SET @CTotalActivo = (SELECT Columna FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL ACTIVO')
	SET @CTotalPasivoPatrimonio = (SELECT Columna FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO')

	IF(@CTotalActivo != @CTotalPasivoPatrimonio)
	BEGIN
		IF(@CTotalActivo > @CTotalPasivoPatrimonio)
		BEGIN
			WHILE @CTotalActivo != @CTotalPasivoPatrimonio
			BEGIN
			INSERT INTO #EstadoSituacionFinanciera VALUES (NULL, NULL, NULL, NULL, NULL, '2', 'Activo', 1, NULL, NULL, 0, 0, NULL, 3, 'BLANCO', NULL, NULL, 1, @CTotalPasivoPatrimonio)
			SET @CTotalPasivoPatrimonio = @CTotalPasivoPatrimonio + 1
			UPDATE #EstadoSituacionFinanciera
			SET Columna = @CTotalPasivoPatrimonio
			WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO'
			UPDATE #EstadoSituacionFinanciera
			SET Columna = Columna + 1
			WHERE TypeAccountId = 4 AND DeudoraAcreedora = '2'
			END
		END
		ELSE IF(@CTotalActivo < @CTotalPasivoPatrimonio)
		BEGIN
			WHILE @CTotalActivo != @CTotalPasivoPatrimonio
			BEGIN
			INSERT INTO #EstadoSituacionFinanciera VALUES (NULL, NULL, NULL, NULL, NULL, '1', 'Activo', 1, NULL, NULL, 0, 0, NULL, 3, 'BLANCO', NULL, NULL, 1, @CTotalActivo)
			SET @CTotalActivo = @CTotalActivo + 1
			UPDATE #EstadoSituacionFinanciera
			SET Columna = @CTotalActivo
			WHERE Descripcion = 'TOTAL ACTIVO'
			UPDATE #EstadoSituacionFinanciera
			SET Columna = Columna + 1
			WHERE TypeAccountId = 4 AND DeudoraAcreedora = '1'
			END
		END
	END

	SET @CTotalActivoContingentes = (SELECT Columna FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL ACTIVO + CONTINGENTES')
	SET @CTotalPasivoPatrimonioContingentes = (SELECT Columna FROM #EstadoSituacionFinanciera WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO + CONTINGENTES')

	IF(@CTotalActivoContingentes != @CTotalPasivoPatrimonioContingentes)
	BEGIN
		IF(@CTotalActivoContingentes > @CTotalPasivoPatrimonioContingentes)
		BEGIN
			WHILE @CTotalActivoContingentes != @CTotalPasivoPatrimonioContingentes
			BEGIN
			INSERT INTO #EstadoSituacionFinanciera VALUES (NULL, NULL, NULL, NULL, NULL, '2', 'Activo', 1, NULL, NULL, 0, 0, NULL, 3, 'BLANCO', NULL, NULL, 1, @CTotalPasivoPatrimonioContingentes)
			SET @CTotalPasivoPatrimonioContingentes = @CTotalPasivoPatrimonioContingentes + 1
			UPDATE #EstadoSituacionFinanciera
			SET Columna = @CTotalPasivoPatrimonioContingentes
			WHERE Descripcion = 'TOTAL ACTIVO + CONTINGENTES'
			END
		END
		ELSE IF(@CTotalActivoContingentes < @CTotalPasivoPatrimonioContingentes)
		BEGIN
			WHILE @CTotalActivoContingentes != @CTotalPasivoPatrimonioContingentes
			BEGIN
			INSERT INTO #EstadoSituacionFinanciera VALUES (NULL, NULL, NULL, NULL, NULL, '1', 'Activo', 1, NULL, NULL, 0, 0, NULL, 3, 'BLANCO', NULL, NULL, 1, @CTotalActivoContingentes)
			SET @CTotalActivoContingentes = @CTotalActivoContingentes + 1
			UPDATE #EstadoSituacionFinanciera
			SET Columna = @CTotalActivoContingentes
			WHERE Descripcion = 'TOTAL PASIVO + PATRIMONIO + CONTINGENTES'
			END
		END
	END

	SELECT * FROM #EstadoSituacionFinanciera T0
	ORDER BY T0.TypeAccountId, T0.Columna
	--SELECT * FROM #BalanceSaldo
	
END		