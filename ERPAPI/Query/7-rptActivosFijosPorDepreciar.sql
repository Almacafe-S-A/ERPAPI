/****** Object:  StoredProcedure [dbo].[rptActivosFijosPorDepreciar]    Script Date: 12/2/2020 10:22:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER   PROCEDURE [dbo].[rptActivosFijosPorDepreciar]
	@ANIO INT,
	@CENTROCOSTO BIGINT
AS
BEGIN		
	IF(@CENTROCOSTO = 0)
	BEGIN
	SELECT T1.FixedAssetGroupDescription
, T0.AssetDate
, T0.FixedAssetName
, T0.FixedAssetDescription
, T0.WareHouseName
, T0.Amount
, T0.Cost
, T0.ResidualValue
, T0.ToDepreciate
, T0.FixedActiveLife
, T2.Year
, T2.January
, T2.February
, T2.March
, T2.April
, T2.May
, T2.June
, T2.July
, T2.August
, T2.September
, T2.October
, T2.November
, T2.December
, COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0)
, 
CASE
WHEN T0.FixedActiveLife IS NULL THEN NULL
WHEN T0.FixedActiveLife = 0 THEN NULL
WHEN COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0) = T0.ToDepreciate THEN NULL
WHEN COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0) = T0.ToDepreciate THEN NULL
ELSE T0.ToDepreciate - COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0)
END AS 'PorDepreciar'
FROM FixedAsset T0
INNER JOIN FixedAssetGroup T1 ON T0.FixedAssetGroupId = T0.FixedAssetGroupId
LEFT JOIN DepreciationFixedAsset T2 ON T2.FixedAssetId = T0.FixedAssetId
WHERE T2.Year = @ANIO
	END
	ELSE
	BEGIN
		SELECT T1.FixedAssetGroupDescription
, T0.AssetDate
, T0.FixedAssetName
, T0.FixedAssetDescription
, T0.WareHouseName
, T0.Amount
, T0.Cost
, T0.ResidualValue
, T0.ToDepreciate
, T0.FixedActiveLife
, T2.Year
, T2.January
, T2.February
, T2.March
, T2.April
, T2.May
, T2.June
, T2.July
, T2.August
, T2.September
, T2.October
, T2.November
, T2.December
, COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0)
, 
CASE
WHEN T0.FixedActiveLife IS NULL THEN NULL
WHEN T0.FixedActiveLife = 0 THEN NULL
WHEN COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0) = T0.ToDepreciate THEN NULL
WHEN COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0) = T0.ToDepreciate THEN NULL
ELSE T0.ToDepreciate - COALESCE(T2.TotalDepreciated, T0.TotalDepreciated, 0)
END AS 'PorDepreciar'
FROM FixedAsset T0
INNER JOIN FixedAssetGroup T1 ON T0.FixedAssetGroupId = T0.FixedAssetGroupId
LEFT JOIN DepreciationFixedAsset T2 ON T2.FixedAssetId = T0.FixedAssetId
WHERE T2.Year = @ANIO
AND T0.CenterCostId = @CENTROCOSTO
	END
END											
