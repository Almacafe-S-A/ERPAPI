SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE ERP
GO


CREATE OR ALTER   PROCEDURE [dbo].[GenerarEstadoCambiosPatrimonio]
	@MES INT,
	@ANIO INT,
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
	DECLARE @NIVEL INT;
	DECLARE @ANIOPREV INT;
	DECLARE @ANIOANTPREV INT;
	DECLARE @FechaFinHaceDosAnios AS DATE;
	DECLARE @SaldoHaceDosAnios AS FLOAT;
	DECLARE @DebeHaceDosAnios FLOAT;
	DECLARE @HaberHaceDosAnios FLOAT;

	SET @NIVEL = 4;
	SET @ANIOPREV = @ANIO - 1;
	SET @ANIOANTPREV = @ANIO - 2;


	DROP TABLE IF EXISTS #BalanceSaldo;	
	DROP TABLE IF EXISTS #BalanceSaldoAnt;	
	DROP TABLE IF EXISTS #BalanceSaldoPrevAnt;
	
	DECLARE @BalanceSaldo Table (
			AccountId INT,
		AccountCode NVARCHAR(50),
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		Totaliza BIT,
		SaldoPrevAnio FLOAT,
		SaldoPrev FLOAT,
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT,
		HierarchyAccount BIGINT
	);

	DECLARE @BalanceSaldoAnt Table (
		AccountId INT,
		AccountCode NVARCHAR(50),
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		Totaliza BIT,
		SaldoPrevAnio FLOAT,
		SaldoPrev FLOAT,
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT,
		HierarchyAccount BIGINT
	);

	DECLARE @BalanceSaldoPrevAnt Table (
		AccountId INT,
		AccountCode NVARCHAR(50),
		AccountName NVARCHAR(200),
		ParentAccountId INT,
		DeudoraAcreedora NVARCHAR(MAX),
		Estado NVARCHAR(MAX),
		Totaliza BIT,
		SaldoPrevAnio FLOAT,
		SaldoPrev FLOAT,
		Debe FLOAT,
		Haber FLOAT,
		SaldoFinal FLOAT,
		HierarchyAccount BIGINT
	);

	DROP TABLE IF EXISTS #EstadoCambiosPatrimonio;	

	CREATE TABLE #EstadoCambiosPatrimonio (
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
		Columna INT,
		SaldoHaceDosAnios FLOAT,
		DebeHaceDosAnios FLOAT,
		HaberHaceDosAnios FLOAT
	);
	
    ----Carga saldos de año actual
		INSERT @BalanceSaldo EXEC BalanceDeSaldos
										@MES,
										@ANIO,
										@NIVEL,
										@CENTROCOSTO 

	----Carga saldos de año anterior	
		INSERT @BalanceSaldoAnt EXEC BalanceDeSaldos
										12,
										@ANIOPREV,
										@NIVEL,
										@CENTROCOSTO 
		----Carga saldos de año actual previo al anterior
		INSERT @BalanceSaldoPrevAnt EXEC BalanceDeSaldos
										12,
										@ANIOANTPREV,
										@NIVEL,
										@CENTROCOSTO
										
		INSERT INTO #EstadoCambiosPatrimonio
				SELECT 
				Nota = CASE WHEN B.HierarchyAccount != 2 THEN NULL ELSE ROW_NUMBER() OVER (PARTITION BY CASE WHEN B.HierarchyAccount != 2 THEN 1 ELSE 0 END ORDER BY B.AccountId) END + 3,
				B.AccountId, B.AccountCode, B.AccountName AS 'Descripcion', B.ParentAccountId, 
				CASE
				WHEN B.DeudoraAcreedora = 'A' THEN '2'
				ELSE '1'
				END AS 'DeudoraAcreedora', B.Estado, B.Totaliza, 
				Round(B1.SaldoFinal,2) AS 'AñoAnterior', 
				Round(B1.Haber,2) SaldoPrev, 
				Round(B.Debe,2) Debe, 
				Round(B.Haber,2) Haber, 
				Round(B.SaldoFinal,2) AS 'AñoActual'
				, T2.TypeAccountId
				, T3.TypeAccountName AS 'TipoDeCuenta'
				, T4.ParentAccountId AS 'SubCuentaId'
				, T5.Description AS 'SubCuenta'
				, B.HierarchyAccount AS 'Nivel'
				, ROW_NUMBER() OVER (PARTITION BY B.DeudoraAcreedora ORDER BY B.AccountCode) AS 'Columna'
				, Round(B2.SaldoFinal,2) AS 'SaldoHaceDosAnios'
				, Round(B2.Debe,2) AS 'DebeHaceDosAnios'
				, Round(B2.Haber,2) AS 'HaberHaceDosAnios'
				FROM @BalanceSaldo B 
				INNER JOIN @BalanceSaldoPrevAnt B2 ON B2.AccountCode = B.AccountCode 
				INNER JOIN 	@BalanceSaldoAnt B1 ON B1.AccountCode = B.AccountCode
				LEFT JOIN Accounting T2 ON B.AccountId = T2.AccountId
				LEFT JOIN TypeAccount T3 ON T2.TypeAccountId = T3.TypeAccountId
				LEFT JOIN Accounting T4 ON T4.AccountId = T2.ParentAccountId
				LEFT JOIN Accounting T5 ON T4.ParentAccountId = T5.AccountId
				WHERE B.HierarchyAccount <= @NIVEL
				AND NOT (Round(B.SaldoFinal,2) = 0 AND Round(B.SaldoPrevAnio,2) = 0 AND B.HierarchyAccount > 2)
				AND NOT (B.HierarchyAccount = 1 AND T2.TypeAccountId = 4)
				AND NOT (B.HierarchyAccount > 2 AND T2.TypeAccountId = 4)
				ORDER BY B.AccountCode


		UPDATE #EstadoCambiosPatrimonio
			SET 
			Descripcion = 'RESULTADOS DE EJERCICIOS ANTERIORES',
			AñoAnterior = COALESCE((SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			SaldoPrev = COALESCE((SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			Debe = COALESCE((SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			Haber = COALESCE((SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			AñoActual = COALESCE((SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			SaldoHaceDosAnios = COALESCE((SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			DebeHaceDosAnios = COALESCE((SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			HaberHaceDosAnios = COALESCE((SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
			AccountCode = '32401/32402',
			Nivel = 5
			WHERE AccountCode = '324'

			UPDATE #EstadoCambiosPatrimonio
			SET Columna = Columna + 1
			WHERE Columna > (SELECT Columna FROM #EstadoCambiosPatrimonio WHERE AccountCode = '324')

			UPDATE #EstadoCambiosPatrimonio
			SET 
			Descripcion = 'RESULTADOS DEL EJERCICIO',
			AñoAnterior = COALESCE((SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			SaldoPrev = COALESCE((SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			Debe = COALESCE((SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			Haber = COALESCE((SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			AñoActual = COALESCE((SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			SaldoHaceDosAnios = COALESCE((SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			DebeHaceDosAnios = COALESCE((SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			HaberHaceDosAnios = COALESCE((SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
			AccountCode = '32501/32502',
			Nivel = 5
			WHERE AccountCode = '325'

			INSERT INTO #EstadoCambiosPatrimonio 
			SELECT Nota, AccountId, NULL, 'TOTAL PATRIMONIO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
			, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, (SELECT MAX(Columna) + 1 FROM #EstadoCambiosPatrimonio WHERE TypeAccountId = 3), SaldoHaceDosAnios, DebeHaceDosAnios, HaberHaceDosAnios
			FROM #EstadoCambiosPatrimonio WHERE AccountCode = '3'


		select *  from #EstadoCambiosPatrimonio EC WHERE EC.AccountCode LIKE '3%' AND EC.AccountCode NOT IN ('3','32301','31101','32401','32501') OR EC.AccountCode IS NULL


	
END