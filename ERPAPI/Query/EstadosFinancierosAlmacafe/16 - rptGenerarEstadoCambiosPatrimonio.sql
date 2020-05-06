/****** Object:  StoredProcedure [dbo].[GenerarEstadoCambiosPatrimonio]    Script Date: 5/5/2020 09:35:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER     PROCEDURE [dbo].[GenerarEstadoCambiosPatrimonio]
	@MES INT,
	@ANIO INT,
	@CENTROCOSTO BIGINT
AS
BEGIN		
	DECLARE @NIVEL INT;
	DECLARE @ANIOPREV INT;
	DECLARE @ANIOANTPREV INT;

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
		INSERT @BalanceSaldo EXEC BalanceDeSaldos2
										@MES,
										@ANIO,
										@NIVEL,
										@CENTROCOSTO ,
										@ANIO

		----Establece la suma del debe y haber anio actual
		UPDATE @BalanceSaldo SET  Debe = (select sum(jel.debit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
															where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIO)+'-01-01' AND sTR(@ANIO+1)+'-01-01') , 
		Haber = (select sum(jel.Credit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
												where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIO)+'-01-01' AND sTR(@ANIO+1)+'-01-01') 
		WHERE AccountCode = '324' or AccountCode = '32401'
		
		------Actualiza el resultado del ejercicio dependiendo si es utilidad o perdida
		update @BalanceSaldo set Debe  = case when SaldoFinal < 0 then SaldoFinal  else 0 end , haber = case when saldofinal>0 then SaldoFinal else  0 end
			where AccountCode = '325'
		-------actualiza la suma del debe y el haber 
		update @BalanceSaldo set Debe = (select sum(Debe) from @BalanceSaldo where AccountCode = '325' or AccountCode = '324' ),
		Haber = (select sum(Haber) from @BalanceSaldo where AccountCode = '325' or AccountCode = '324' )		
		where AccountCode = '3' or AccountCode = '32'

	----Carga saldos de año anterior	
		INSERT @BalanceSaldoAnt EXEC BalanceDeSaldos2
										@MES,
										@ANIOPREV,
										@NIVEL,
										@CENTROCOSTO,
										@ANIO


		UPDATE @BalanceSaldoAnt SET  Debe = 0 , Haber = 0  WHERE AccountId =921
		--Actualiza la suma del debe y el haber
		UPDATE @BalanceSaldoAnt SET  Debe = (select sum(jel.debit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND sTR(@ANIO)+'-01-01') , 
									Haber = (select sum(jel.Credit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND sTR(@ANIO)+'-01-01') 
		WHERE AccountId = 921 or Accountid= 920 or AccountCode = '3' or AccountCode = '32'
		----Establece la suma del debe y haber anio actual
		update @BalanceSaldoAnt set SaldoFinal = SaldoFinal + (select abs(saldofinal) from  @BalanceSaldoAnt where AccountCode = '325') where AccountCode = '32' or AccountCode = '3'
		-----Quita los resultados del debe y el haber
		UPDATE @BalanceSaldoAnt set debe = 0 , Haber = 0, SaldoFinal = 0 where AccountCode like '325%' 


		----Carga saldos de año actual previo al anterior
		INSERT @BalanceSaldoPrevAnt EXEC BalanceDeSaldos2
										@MES,
										@ANIOANTPREV,
										@NIVEL,
										@CENTROCOSTO,
										@ANIO
		--UPDATE @BalanceSaldoAnt SET  Debe = 0 , Haber = 0, SaldoFinal = 0 WHERE AccountId =924

		INSERT INTO #EstadoCambiosPatrimonio
				SELECT 
				Nota = CASE WHEN B.HierarchyAccount != 2 THEN NULL ELSE ROW_NUMBER() OVER (PARTITION BY CASE WHEN B.HierarchyAccount != 2 THEN 1 ELSE 0 END ORDER BY B.AccountId) END + 3,
				B.AccountId, B.AccountCode, B.AccountName AS 'Descripcion', B.ParentAccountId, 
				CASE
				WHEN B.DeudoraAcreedora = 'A' THEN '2'
				ELSE '1'
				END AS 'DeudoraAcreedora', B.Estado, B.Totaliza, 
				Round(B1.SaldoFinal,2) AS 'AñoAnterior', 
				Round(B1.SaldoPrev,2) SaldoPrev, 
				Round(b.Debe,2) Debe, 
				Round(B.haber,2) Haber, 
				Round(B.SaldoFinal,2) AS 'AñoActual'
				, T2.TypeAccountId
				, T3.TypeAccountName AS 'TipoDeCuenta'
				, T4.ParentAccountId AS 'SubCuentaId'
				, T5.Description AS 'SubCuenta'
				, B.HierarchyAccount AS 'Nivel'
				, ROW_NUMBER() OVER (PARTITION BY B.DeudoraAcreedora ORDER BY B.AccountCode) AS 'Columna'
				, Round(B2.SaldoFinal,2) AS 'SaldoHaceDosAnios'
				, Round(B1.DEBE,2) AS 'DebeHaceDosAnios'
				, Round(B1.Haber,2) AS 'HaberHaceDosAnios'
				FROM @BalanceSaldo B 
				INNER JOIN @BalanceSaldoPrevAnt B2 ON B2.AccountCode = B.AccountCode 
				INNER JOIN 	@BalanceSaldoAnt B1 ON B1.AccountCode = B.AccountCode
				LEFT JOIN Accounting T2 ON B.AccountId = T2.AccountId
				LEFT JOIN TypeAccount T3 ON T2.TypeAccountId = T3.TypeAccountId
				LEFT JOIN Accounting T4 ON T4.AccountId = T2.ParentAccountId
				LEFT JOIN Accounting T5 ON T4.ParentAccountId = T5.AccountId
				WHERE B.HierarchyAccount <= @NIVEL
				--AND NOT (Round(B.SaldoFinal,2) = 0 AND Round(B.SaldoPrevAnio,2) = 0 AND B.HierarchyAccount > 2)
				AND NOT (B.HierarchyAccount = 1 AND T2.TypeAccountId = 4)
				AND NOT (B.HierarchyAccount > 2 AND T2.TypeAccountId = 4)
				ORDER BY B.AccountCode
				--WHERE B.HierarchyAccount <= @NIVEL
				--AND T2.TypeAccountId = 3
				----AND NOT (b.Debe+b.Haber+b.SaldoFinal+B1.Debe+B1.Haber+B1.SaldoFinal+b2.SaldoFinal+B2.Debe+b2.Haber)=0
				--AND NOT (B.HierarchyAccount = 1 AND T2.TypeAccountId = 4)
				--AND NOT (B.HierarchyAccount > 2 AND T2.TypeAccountId = 4)
				--ORDER BY B.AccountCode

		



		--UPDATE #EstadoCambiosPatrimonio
		--	SET 
		--	Descripcion = 'RESULTADOS DE EJERCICIOS ANTERIORES',
		--	AñoAnterior = COALESCE((SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	SaldoPrev = COALESCE((SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	Debe = COALESCE((SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	Haber = COALESCE((SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	AñoActual = COALESCE((SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	SaldoHaceDosAnios = COALESCE((SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	DebeHaceDosAnios = COALESCE((SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	HaberHaceDosAnios = COALESCE((SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32401'), (SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32402'), 0),
		--	AccountCode = '32401/32402',
		--	Nivel = 3

		--	WHERE AccountCode = '324'

		--	UPDATE #EstadoCambiosPatrimonio
		--	SET Columna = Columna + 1
		--	WHERE Columna > (SELECT Columna FROM #EstadoCambiosPatrimonio WHERE AccountCode = '324')

		--	UPDATE #EstadoCambiosPatrimonio
		--	SET 
		--	Descripcion = 'RESULTADOS DEL EJERCICIO',
		--	AñoAnterior = COALESCE((SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT AñoAnterior FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	SaldoPrev = COALESCE((SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT SaldoPrev FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	Debe = COALESCE((SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT Debe FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	Haber = COALESCE((SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT Haber FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	AñoActual = COALESCE((SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT AñoActual FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	SaldoHaceDosAnios = COALESCE((SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT SaldoHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	DebeHaceDosAnios = COALESCE((SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT DebeHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	HaberHaceDosAnios = COALESCE((SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32501'), (SELECT HaberHaceDosAnios FROM #EstadoCambiosPatrimonio WHERE AccountCode = '32502'), 0),
		--	AccountCode = '32501/32502',
		--	Nivel = 3
		--	WHERE AccountCode = '325'



			INSERT INTO #EstadoCambiosPatrimonio 
			SELECT Nota, AccountId, NULL, 'TOTAL PATRIMONIO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
			, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, 100, SaldoHaceDosAnios, DebeHaceDosAnios, HaberHaceDosAnios
			FROM #EstadoCambiosPatrimonio WHERE AccountCode = '3'

			DELETE FROM #EstadoCambiosPatrimonio WHERE AccountCode = '3'


		select *  from #EstadoCambiosPatrimonio EC where TypeAccountId = 3 and Nivel <@NIVEL-- WHERE EC.AccountCode LIKE '3%' AND EC.AccountCode NOT IN ('3','32301','31101','32401','32501') OR EC.AccountCode IS NULL


	
END