USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[rptMovimientosHistoricos]    Script Date: 28/1/2020 14:28:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[rptMovimientosHistoricos]
    @MES INT,
	@ANIO INT,	
	@CENTROCOSTO BIGINT
AS
BEGIN		
SELECT T0.Date
, T0.FechaCierre
, T0.Memo
, T1.CostCenterId
, T1.CostCenterName
, T1.AccountId
, T1.AccountName
, T1.Debit
, T1.Credit
INTO #Table00
FROM CierresJournal T0
INNER JOIN CierresJournalEntryLine T1 ON T0.CierresJournalEntryId = T1.CierresJournalEntryLineId
WHERE YEAR(T0.FechaCierre) = @ANIO AND MONTH(T0.FechaCierre) = @MES

IF(@CENTROCOSTO = 0)
BEGIN
SELECT * FROM #Table00
END
ELSE
BEGIN
SELECT * FROM #Table00 WHERE CostCenterId = @CENTROCOSTO
END

DROP TABLE #Table00
END											
GO


