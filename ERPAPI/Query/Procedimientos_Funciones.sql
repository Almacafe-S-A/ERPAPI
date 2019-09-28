ALTER FUNCTION [dbo].[SumaCredito]
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


ALTER FUNCTION [dbo].[SumaDebito]
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

SELECT a.AccountId,a.AccountName,a.ParentAccountId
  ,SUM(jel.Credit) AS Credit,SUM(jel.Debit) AS Debit
  ,SUM(jel.Debit) - SUM(jel.Credit) AS AccountBalance
  FROM Accounting a  
LEFT JOIN  JournalEntryLine jel
   ON a.AccountId = jel.AccountId
  GROUP BY a.AccountId,a.AccountName,a.ParentAccountId


  SELECT a.AccountId,a.AccountName,a.ParentAccountId 
    , SUM(jel.Credit) AS Credit, SUM(jel.Debit) AS Debit 
    , SUM(jel.Debit) - SUM(jel.Credit) AS AccountBalance 
    FROM Accounting a                                        
    LEFT JOIN  JournalEntryLine jel              
    ON a.AccountId = jel.AccountId  
     LEFT JOIN JournalEntry je  
       ON jel.JournalEntryId = je.JournalEntryId                   
    WHERE je.Date>= '2019-09-28' 
    and je.Date<='2019-09-28' GROUP BY a.AccountId, a.AccountName,a.ParentAccountId

