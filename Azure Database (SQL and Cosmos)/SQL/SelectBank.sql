SELECT * FROM [Bank].[AccountType]
SELECT * FROM [Bank].[Customers]
SELECT * FROM [Bank].[Accounts]
Select * FROM [Bank].[Transaction]
 --Ovo bi prikazivalo za sve ziro racune u sistemu ime vlasnika racuna i stanje na tom racunu
SELECT c.FirstName, sum(t.valuee)
FROM [Bank].[Customers] c
INNER JOIN 
[Bank].[Accounts] acc 
ON c.Jmbg = acc.CustomerJmbg
INNER JOIN
[Bank].[AccountType] at
ON acc.AccountTypeId = at.id
INNER JOIN
[Bank].[Transaction] t
ON t.accountid = acc.id
WHERE at.AccountTypeName = 'Ziro racun'
group by c.firstname

--ovo prikazuje za jednog konkretnog klijenta sve njegove racune i stanje na njima
SELECT c.FirstName, at.AccountTypeName, ISNULL(sum(t.valuee),0)
FROM [Bank].[Customers] c
INNER JOIN 
[Bank].[Accounts] acc 
ON c.Jmbg = acc.CustomerJmbg
INNER JOIN
[Bank].[AccountType] at
ON acc.AccountTypeId = at.id
LEFT OUTER JOIN
[Bank].[Transaction] t
ON t.accountid = acc.id
WHERE c.Jmbg = '123456789012'
group by c.Jmbg, c.firstname, at.AccountTypeName