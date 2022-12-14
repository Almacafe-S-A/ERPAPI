USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[GenerarEstadoCambiosPatrimonio]    Script Date: 23/5/2020 11:43:12 ******/
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
	DECLARE @FechaFinAnio varchar(30);
	DECLARE @FechaFinAnioPrev varchar(30);
	DECLARE @FechaFinAnioPrevAnt varchar(30);

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

	DROP TABLE IF EXISTS #PartExcluidas;	
	CREATE TABLE #PartExcluidas(
		Anio INT,
		JournalEntryId BIGINT
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

	DROP TABLE IF EXISTS #EstadoCambiosPatrimonio2;	

	CREATE TABLE #EstadoCambiosPatrimonio2 (
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
		HaberHaceDosAnios FLOAT,
		FechaLimiteAnio Date,
		FechaLimiteAnioPrev Date,
		FechaLimiteAnioPrevAnt Date
	);



	IF @MES = 12
		BEGIN
			SET @FechaFinAnio = LTRIM(STR(@ANIO+1) + '-01-01');
		END
	ELSE
		BEGIN
			SET @FechaFinAnio =LTRIM(STR(@ANIO) + '-' + STR(@MES+1) + '-01');
		END
	
			SET @FechaFinAnioPrev = STR(@ANIO)+'-01-01';
			SET @FechaFinAnioPrevAnt = STR(@ANIOPREV)+'-01-01';


	print @FechaFinAnio
	print @FechaFinAnioPrev
	print @FechaFinAnioPrevAnt
	IF  @ANIO >2018 
	BEGIN
		--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,2016); 
		INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5471);
		INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5472);
		INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5473);
	END
	ELSE
		BEGIN		
			--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,2656);
			--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,3270);
			--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,1748);
			--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,2655); 
			--INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,2015);
			INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2018,2016); 
			INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5471);
			INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5472);
			INSERT INTO #PartExcluidas (Anio, JournalEntryId) VALUES (2019,5473);
		END
    ----Carga saldos de año actual
		INSERT @BalanceSaldo EXEC BalanceDeSaldos2
										@MES,
										@ANIO,
										@NIVEL,
										@CENTROCOSTO ,
										@ANIO
										
		----Establece la suma del debe y haber anio actual
		UPDATE @BalanceSaldo SET  Debe = ISNULL((select sum(jel.debit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
									where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIO)+'-01-01' AND @FechaFinAnio and je.EstadoId = 6
									and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)
									AND NOT EXISTS(SELECT 1 FROM #PartExcluidas E WHERE E.Anio = @ANIO AND je.JournalEntryId = E.JournalEntryId)),0), 
		Haber =ISNULL( (select sum(jel.Credit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
												where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIO)+'-01-01' AND @FechaFinAnio and je.EstadoId = 6
												and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)
												AND NOT EXISTS(SELECT 1 FROM #PartExcluidas E WHERE E.Anio = @ANIO AND je.JournalEntryId = E.JournalEntryId)),0) 
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
										12,
										@ANIOPREV,
										@NIVEL,
										@CENTROCOSTO,
										@ANIO



		UPDATE @BalanceSaldoAnt SET  Debe = 0 , Haber = 0  WHERE AccountId =921
		--Actualiza la suma del debe y el haber
		UPDATE @BalanceSaldoAnt SET  Debe =isnull( (select sum(jel.debit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND @FechaFinAnioPrev and je.EstadoId = 6
																and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)),0),
									Haber =isnull( (select sum(jel.Credit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND @FechaFinAnioPrev and je.EstadoId = 6
																and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)),0)
									
									
		WHERE AccountId = 921 or Accountid= 920 or AccountCode = '3' or AccountCode = '32'
		----Establece la suma del debe y haber anio actual
		update @BalanceSaldoAnt set SaldoFinal = SaldoFinal + (select abs(saldofinal) from  @BalanceSaldoAnt where AccountCode = '325') where AccountCode = '32' or AccountCode = '3' or AccountCode = '324'
		-----Quita los resultados del debe y el haber
		UPDATE @BalanceSaldoAnt set debe = 0 , Haber = 0, SaldoFinal = 0 where AccountCode like '325%' 






		----Carga saldos de año actual previo al anterior
		INSERT @BalanceSaldoPrevAnt EXEC BalanceDeSaldos2
										12,
										@ANIOANTPREV,
										@NIVEL,
										@CENTROCOSTO,
										@ANIO
		UPDATE @BalanceSaldoPrevAnt SET  Debe = 0 , Haber = 0  WHERE AccountId =921
		--Actualiza la suma del debe y el haber
		UPDATE @BalanceSaldoPrevAnt SET  Debe =isnull( (select sum(jel.debit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND @FechaFinAnioPrev and je.EstadoId = 6
																and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)),0),
									Haber =isnull( (select sum(jel.Credit) from JournalEntryLine jel inner join JournalEntry je on je.JournalEntryId = jel.JournalEntryId
																where jel.AccountId = 921 and je.DatePosted BETWEEN sTR(@ANIOPREV)+'-01-01' AND @FechaFinAnioPrev and je.EstadoId = 6
																and  not exists (select * from JournalEntryCanceled where ReverseJournalEntryId = jel.JournalEntryId or CanceledJournalentryId = jel.JournalEntryId)),0)
									
									
		WHERE AccountId = 921 or Accountid= 920 or AccountCode = '3' or AccountCode = '32'
		----Establece la suma del debe y haber anio actual
		update @BalanceSaldoPrevAnt set SaldoFinal = SaldoFinal + (select abs(saldofinal) from  @BalanceSaldoAnt where AccountCode = '325') where AccountCode = '32' or AccountCode = '3'
		-----Quita los resultados del debe y el haber
		UPDATE @BalanceSaldoPrevAnt set debe = 0 , Haber = 0, SaldoFinal = 0 where AccountCode like '325%' 

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
				AND NOT (Round(B.SaldoFinal,2) = 0 AND Round(B.SaldoPrevAnio,2) = 0 AND B.HierarchyAccount > 2)
				AND NOT (B.HierarchyAccount = 1 AND T2.TypeAccountId = 4)
				AND NOT (B.HierarchyAccount > 2 AND T2.TypeAccountId = 4)				
				ORDER BY B.AccountCode


			INSERT INTO #EstadoCambiosPatrimonio 
			SELECT Nota, AccountId, NULL, 'TOTAL PATRIMONIO', ParentAccountId, DeudoraAcreedora, Estado, Totaliza, AñoAnterior, SaldoPrev, Debe, Haber, AñoActual
			, TypeAccountId, 'TOTAL', SubCuentaId, SubCuenta, Nivel, 100, SaldoHaceDosAnios, DebeHaceDosAnios, HaberHaceDosAnios
			FROM #EstadoCambiosPatrimonio WHERE AccountCode = '3'

			DELETE FROM #EstadoCambiosPatrimonio WHERE AccountCode = '3'

		INSERT INTO #EstadoCambiosPatrimonio2 
		select	Nota
				,AccountId
				,AccountCode
				,Descripcion
				,ParentAccountId
				,DeudoraAcreedora
				,Estado
				,Totaliza
				,AñoAnterior
				,SaldoPrev
				,Debe
				,Haber
				,AñoActual
				,TypeAccountId
				,TipoDeCuenta
				,SubCuentaId
				,SubCuenta
				,Nivel
				,Columna
				,SaldoHaceDosAnios
				,DebeHaceDosAnios
				,HaberHaceDosAnios
				,EOMONTH(LTRIM(STR(@ANIO) + '-' + STR(@MES) + '-01'))
				,EOMONTH(LTRIM(STR(@ANIO-1) + '-' + STR(@MES) + '-01'))
				,EOMONTH(LTRIM(STR(@ANIO-2) + '-' + STR(@MES) + '-01'))
  from #EstadoCambiosPatrimonio EC where TypeAccountId = 3  and Nivel <@NIVEL


		UPDATE #EstadoCambiosPatrimonio2 SET SaldoHaceDosAnios =Isnull((select  SUM(SaldoHaceDosAnios) from #EstadoCambiosPatrimonio2 es 
										WHERE  es.Nivel > 2  and es.ParentAccountId = S.AccountId ),0),
										AñoAnterior =Isnull((select  SUM(AñoAnterior) from #EstadoCambiosPatrimonio2 es 
										WHERE  es.Nivel > 2  and es.ParentAccountId = S.AccountId ),0)
		FROM #EstadoCambiosPatrimonio2 S  where S.Nivel< 3

		update #EstadoCambiosPatrimonio2 set AñoActual = (select  SUM(AñoActual) from #EstadoCambiosPatrimonio2 where Nivel = 2) 
				, AñoAnterior = (select  SUM(AñoAnterior) from #EstadoCambiosPatrimonio2 where Nivel = 2 ) 
				, SaldoHaceDosAnios = (select  SUM(SaldoHaceDosAnios) from #EstadoCambiosPatrimonio2 where Nivel = 2) 
				where AccountId = 898

		SELECT * FROM  #EstadoCambiosPatrimonio2 order by Columna

	
END