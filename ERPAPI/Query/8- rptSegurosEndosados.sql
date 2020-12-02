USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[rptSegurosEndosados]    Script Date: 16/1/2020 16:57:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rptSegurosEndosados]
    @MES INT,
	@ANIO INT,	
	@CENTROCOSTO BIGINT
AS
BEGIN		
	SELECT T0.NoPoliza
, T0.BeginDateofInsurance
, T0.EndDateofInsurance
, T1.InsurancesId
, T1.InsurancesName
, T2.CustomerId
, T2.Customername
, T2.ProductdId
, T2.ProductName
, T2.WarehouseId
, T2.WarehouseName
, T2.TotalAmountDl
, T2.TotalAmountLp
, T2.TotalCertificateBalalnce
, T2.TotalAssuredDifernce
, T2.DateGenerated
FROM InsurancesCertificate T0
INNER JOIN Insurances T1 ON T0.InsurancesId = T1.InsurancesId
INNER JOIN InsuranceEndorsement T2 ON T0.NoPoliza = T2.InsurancePolicyId
WHERE YEAR(T2.DateGenerated) = @ANIO AND MONTH(T2.DateGenerated) = @MES
AND T2.CostCenterId IN ( SELECT CASE @CENTROCOSTO
WHEN 0 THEN (SELECT DISTINCT CostCenterId FROM InsuranceEndorsement)
ELSE @CENTROCOSTO END )
END											
GO


