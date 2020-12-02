
/****** Object:  StoredProcedure [dbo].[CargarDepreciaciones]    Script Date: 11/5/2020 08:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Carlos Castillo>
-- Create date: <11-05-2020,,>
-- Description:	<Procedimiento Almacenado para cargar las depreciaciones de los activos cargados la primera vez con sus datos originales,,>
-- =============================================
ALTER     PROCEDURE [dbo].[CargarDepreciaciones]

AS
	
	
BEGIN
	DECLARE @fechaactual  Date;
	DECLARE @codigoactivo		decimal(18,4);
	DECLARE @valorresidual		decimal(18,4);
	DECLARE @adepreciar			decimal(18,4);
	DECLARE @depreciacionmes	decimal(18,4);
	DECLARE @totaldepreciado	decimal(18,4);
	DECLARE @fechaactivo		Date;	
	DECLARE @fechainicial		date;
	DECLARE @factor				int;
	DECLARE @depreciacion		decimal(18,4)


	SET @fechaactual = SYSDATETIME();
	

	delete from DepreciationFixedAsset
	update FixedAsset set NetValue = ActiveTotalCost
	update FixedAsset set AccumulatedDepreciation = 0 , ResidualValue =Round( (ResidualValuePercent/100) * ActiveTotalCost,2 )
	update FixedAsset set   ToDepreciate = Round(ActiveTotalCost - ResidualValue,2)
	update fixedasset set TotalDepreciated = Round( (ToDepreciate / FixedActiveLife )/12,2)
	

	DECLARE cursor_activos CURSOR
		FOR		
			SELECT f.FixedAssetId, 
					f.AssetDate, 
					f.ToDepreciate, 
					f.TotalDepreciated,
					f.AccumulatedDepreciation FROM FixedAsset f

	OPEN cursor_activos;

	FETCH NEXT FROM cursor_activos INTO 
		@codigoactivo,
		@fechaactivo,
		@adepreciar,
		@depreciacionmes,
		@totaldepreciado;

	WHILE @@FETCH_STATUS = 0 
		BEGIN 
			PRINT @codigoactivo
			set @depreciacion = @depreciacionmes;
			set @fechainicial = @fechaactivo;
			WHILE @fechaactivo < @fechaactual AND @totaldepreciado < @adepreciar
				BEGIN 		
							IF (@fechaactivo= @fechainicial and day(@fechainicial)>14)
								SET @depreciacionmes = @depreciacionmes / DAY(@fechainicial)
							ELSE
								SET @depreciacionmes = @depreciacion

							IF ((@adepreciar - @totaldepreciado) < @depreciacion)
								SET @depreciacionmes = (@adepreciar - @totaldepreciado)
							IF EXISTS(SELECT TOP 1 * FROM DepreciationFixedAsset WHERE FixedAssetId = @codigoactivo AND YEAR = YEAR(@fechaactivo)) 							 
								BEGIN
								UPDATE DepreciationFixedAsset SET 
									January  = CASE WHEN MONTH(@fechaactivo) =  1 THEN @depreciacionmes ELSE January END,
									February  = CASE WHEN MONTH(@fechaactivo) =  2 THEN @depreciacionmes ELSE February END,
									March  = CASE WHEN MONTH(@fechaactivo) =  3 THEN @depreciacionmes ELSE March END ,
									April  = CASE WHEN MONTH(@fechaactivo) =  4 THEN @depreciacionmes ELSE April END,
									May  = CASE WHEN MONTH(@fechaactivo) =  5 THEN @depreciacionmes ELSE May END ,
									June  = CASE WHEN MONTH(@fechaactivo) =  6 THEN @depreciacionmes ELSE June END ,
									July  = CASE WHEN MONTH(@fechaactivo) =  7 THEN @depreciacionmes ELSE July END ,
									August  = CASE WHEN MONTH(@fechaactivo) =  8 THEN @depreciacionmes ELSE August END ,
									September  = CASE WHEN MONTH(@fechaactivo) =  9 THEN @depreciacionmes ELSE September END,
									October  = CASE WHEN MONTH(@fechaactivo) =  10 THEN @depreciacionmes ELSE October END ,
									November  = CASE WHEN MONTH(@fechaactivo) =  11 THEN @depreciacionmes ELSE November END ,
									December  = CASE WHEN MONTH(@fechaactivo) =  12 THEN @depreciacionmes ELSE December END
								WHERE FixedAssetId = @codigoactivo AND Year = YEAR(@fechaactivo)
								END
							ELSE
							BEGIN


							INSERT INTO [dbo].[DepreciationFixedAsset]									   
										(FixedAssetId
										,[Year]
									   ,[January]
									   ,[February]
									   ,[March]
									   ,[April]
									   ,[May]
									   ,[June]
									   ,[July]
									   ,[August]
									   ,[September]
									   ,[October]
									   ,[November]
									   ,[December]
									   ,[TotalDepreciated]
									   ,[FechaCreacion]
									   ,[FechaModificacion]
									   ,[UsuarioCreacion]
									   ,[UsuarioModificacion])
								 VALUES				
										(@codigoactivo,
										YEAR(@fechaactivo),
										CASE WHEN MONTH(@fechaactivo) =  1 THEN @depreciacionmes ELSE  0 END,
										CASE WHEN MONTH(@fechaactivo) =  2 THEN @depreciacionmes ELSE  0 END,
										CASE WHEN MONTH(@fechaactivo) =  3 THEN @depreciacionmes ELSE  0 END ,
										CASE WHEN MONTH(@fechaactivo) =  4 THEN @depreciacionmes ELSE  0 END,
										CASE WHEN MONTH(@fechaactivo) =  5 THEN @depreciacionmes ELSE  0 END ,
										CASE WHEN MONTH(@fechaactivo) =  6 THEN @depreciacionmes ELSE  0 END ,
										CASE WHEN MONTH(@fechaactivo) =  7 THEN @depreciacionmes ELSE  0 END ,
										CASE WHEN MONTH(@fechaactivo) =  8 THEN @depreciacionmes ELSE  0 END ,
										CASE WHEN MONTH(@fechaactivo) =  9 THEN @depreciacionmes ELSE  0 END,
										CASE WHEN MONTH(@fechaactivo) =  10 THEN @depreciacionmes ELSE 0  END ,
										CASE WHEN MONTH(@fechaactivo) =  11 THEN @depreciacionmes ELSE 0  END ,
										CASE WHEN MONTH(@fechaactivo) =  12 THEN @depreciacionmes ELSE 0  END									   
									   ,@depreciacionmes
									   ,@fechaactual
									   ,@fechaactual
									   ,'ERP'
									   ,'ERP')

									   
								END

						UPDATE DepreciationFixedAsset  set TotalDepreciated =  January + February + March + April + May + June + July + August + September + October  + November + December 
								where FixedAssetId = @codigoactivo AND YEAR  = YEAR(@fechaactivo); 
						set  @totaldepreciado = (Select sum(totaldepreciated) from DepreciationFixedAsset where FixedAssetId = @codigoactivo) ; 
						
						SET @fechaactivo = DATEADD(MONTH,1,@fechaactivo)
				END 
				UPDATE FixedAsset SET AccumulatedDepreciation = (Select sum(TotalDepreciated) from DepreciationFixedAsset where FixedAssetId = @codigoactivo ) where FixedAssetId = @codigoactivo
				update FixedAsset SET NetValue = ActiveTotalCost- AccumulatedDepreciation where FixedAssetId = @codigoactivo
			FETCH NEXT FROM cursor_activos INTO 
		@codigoactivo,
		@fechaactivo,
		@adepreciar,
		@depreciacionmes,
		@totaldepreciado;

		END
	CLOSE cursor_activos;
	DEALLOCATE cursor_activos ;



	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
END

