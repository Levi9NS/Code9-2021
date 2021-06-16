//URADJEN JE PRIKAZ ZA SVE CUSTOMERE
//U SLUCAJU DA SE TRAZI JEDAN CUSTOMER TREBA DODATI WHERE

//PRVI NACIN

SELECT  c.firstname,ac.type,sum(t.value)
     
  FROM [dbo].[Transaction]t,Account a,AccountType ac,Customer c
  where a.accountTypeId=ac.accountTypeId and t.accountId=a.accountId and c.customerId=a.customerId
  Group by t.accountId,ac.type,c.firstName

//DRUGI NACIN
  
SELECT  c.firstname,ac.type,sum(t.value)
     
  FROM Customer c
  INNER JOIN
  Account as a
  ON a.customerId=c.customerId
  INNER JOIN
  AccountType as ac
  ON ac.accountTypeId=a.accountTypeId
  INNER JOIN
  [dbo].[Transaction] as t
  ON t.accountId=a.accountId
  Group by t.accountId,ac.type,c.firstName