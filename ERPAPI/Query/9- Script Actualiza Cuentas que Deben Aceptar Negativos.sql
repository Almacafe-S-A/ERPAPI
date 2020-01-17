UPDATE Accounting SET AceptaNegativo = 0
GO
UPDATE Accounting SET AceptaNegativo = 1
WHERE AccountName LIKE '(%)'
GO