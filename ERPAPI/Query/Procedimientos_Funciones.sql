﻿CREATE FUNCTION [dbo].[SumaCredito]
(  
	 @FechaInicio DATETIME,
   @FechaFin DATETIME,
	 @cuenta int 
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Credit)       
 FROM JournalEntryLine  T1 
   INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where t1.AccountId = @cuenta
    AND je.Date>=@FechaInicio AND je.Date<=@FechaFin  )

  return @sumatipoisv;
END

GO


CREATE FUNCTION [dbo].[SumaDebito]
(  
   @FechaInicio DATETIME,
   @FechaFin DATETIME,
	 @cuenta int
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Debit)       
 FROM JournalEntryLine  T1 
    INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where t1.AccountId = @cuenta
   AND je.Date>=@FechaInicio AND je.Date<=@FechaFin
  )

  return @sumatipoisv;
END

GO



CREATE FUNCTION [dbo].[TotalDebito]
(  
   @FechaInicio DATETIME,
   @FechaFin DATETIME
	
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Debit)       
 FROM JournalEntryLine  T1 
    INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where  je.Date>=@FechaInicio AND je.Date<=@FechaFin
  )

  return @sumatipoisv;
END

GO

CREATE FUNCTION [dbo].[TotalCredito]
(  
	 @FechaInicio DATETIME,
   @FechaFin DATETIME
	 
)
RETURNS decimal(18,4)-- or whatever length you need
AS
BEGIN
 DECLARE @sumatipoisv as decimal(18,4)
set @sumatipoisv = ( select SUM(T1.Credit)       
 FROM JournalEntryLine  T1 
   INNER JOIN JournalEntry je 
     ON T1.JournalEntryId = je.JournalEntryId
  where 
     je.Date>=@FechaInicio AND je.Date<=@FechaFin  )

  return @sumatipoisv;
END

GO



  SELECT a.AccountId,a.AccountName,a.ParentAccountId 
    , dbo.[SumaCredito]('2019-09-28','2019-09-28',a.AccountId) as Credit
    , dbo.[SumaDebito]('2019-09-28','2019-09-28',a.AccountId) as Debit
    , dbo.[SumaDebito]('2019-09-28','2019-09-28',a.AccountId) -   dbo.[SumaCredito]('2019-09-28','2019-09-28',a.AccountId) AccountBalance 
    FROM Accounting a        GROUP BY a.AccountId, a.AccountName,a.ParentAccountId

