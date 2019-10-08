CREATE FUNCTION [dbo].[SumaCredito]
(  
	 @FechaInicio DATETIME,
   @FechaFin DATETIME,
	 @cuenta INT 
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



SELECT    COALESCE(dbo.[TotalCredito]('2019-10-01','2019-10-01'),0) as TotalCredit , COALESCE(dbo.[TotalDebito]('2019-10-01','2019-10-01'),0) as TotalDebit
  , COALESCE(dbo.[TotalDebito]('2019-10-01','2019-10-01'),0) -   COALESCE(dbo.[TotalCredito]('2019-10-01','2019-10-01'),0) AccountBalance      