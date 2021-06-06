DELETE FROM [Bank].[Transactions] WHERE Id > 0
DELETE FROM [Bank].[Accounts] WHERE Id > 0
DELETE FROM [Bank].[AccountTypes] WHERE Id > 0
DELETE FROM [Bank].[Customers] WHERE Id > 0

DECLARE @typeCA VARCHAR(15) = 'CurrentAccount',
		@typeSA VARCHAR(15) = 'SavingAccount',
		@typeOD VARCHAR(15) = 'Overdraft',
		@typeMG VARCHAR(15) = 'Mortgage',
		@Counter INT = 0,
		@customerId INT,
		@customerNumber INT = 5,
		@accountId INT,
		@accountId2 INT,
		@accountId3 INT,
		@accountId4 INT,
		@accountTypeId INT,
		@typeID1 INT,
		@moneyP INT = 150000,
		@moneyN INT = -10000,
		@selectCustomerId INT,
		@selectAccType INT,
		@createdBy VARCHAR(50) = 'Vukasin'


IF NOT EXISTS (SELECT 1 FROM Bank.AccountTypes WHERE Type = @typeCA)
BEGIN
	INSERT INTO Bank.AccountTypes("Type",ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   
		@typeCA,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	SET @typeID1 = SCOPE_IDENTITY()
	INSERT INTO Bank.AccountTypes("Type",ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   
		@typeSA,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	INSERT INTO Bank.AccountTypes("Type",ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   
		@typeOD,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	INSERT INTO Bank.AccountTypes("Type",ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   
		@typeMG,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	WHILE ( @Counter <= @customerNumber - 1)
		BEGIN
			INSERT INTO Bank.Customers(JMBG,FirstName,LastName,City,PostalCode,Address,HouseNumber,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				'123456789000' + CONVERT(VARCHAR(1), @Counter),
				'FirstName' + CONVERT(VARCHAR(1), @Counter),
				'LastName' + CONVERT(VARCHAR(1), @Counter),
				'City' + CONVERT(VARCHAR(1), @Counter),
				'12345' + CONVERT(VARCHAR(1), @Counter),
				'Address' + CONVERT(VARCHAR(1), @Counter),
				'Number' + CONVERT(VARCHAR(1), @Counter),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @customerId = SCOPE_IDENTITY()

			INSERT INTO Bank.Accounts(Number,CustomerId,AccountTypeId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				'CA0000000000000' + CONVERT(VARCHAR(1), @Counter),
				@customerId,
				@typeID1,
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @accountId = SCOPE_IDENTITY()

			INSERT INTO Bank.Accounts(Number,CustomerId,AccountTypeId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				'SA0000000000000' + CONVERT(VARCHAR(1), @Counter),
				@customerId,
				@typeID1+1,
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @accountId2 = SCOPE_IDENTITY()

			INSERT INTO Bank.Accounts(Number,CustomerId,AccountTypeId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				'OD0000000000000' + CONVERT(VARCHAR(1), @Counter),
				@customerId,
				@typeID1+2,
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @accountId3 = SCOPE_IDENTITY()

			INSERT INTO Bank.Accounts(Number,CustomerId,AccountTypeId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				'MG0000000000000' + CONVERT(VARCHAR(1), @Counter),
				@customerId,
				@typeID1+3,
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @accountId4 = SCOPE_IDENTITY()
			
			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId,
				@moneyP*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @moneyP = @moneyP+10000
			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId,
				@moneyN-10*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)

			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId2,
				@moneyP*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @moneyP = @moneyP+10000
			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId2,
				@moneyN-10*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)

			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId3,
				@moneyP*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @moneyP = @moneyP+10000
			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId3,
				@moneyN-10*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)

			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId4,
				@moneyP*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)
			SET @moneyP = @moneyP+10000
			INSERT INTO Bank.Transactions(AccountId, Value,ChangedBy,ChangedDate,CreatedBy,CreateDate)
			VALUES
			(
				@accountId4,
				@moneyN-10*(@Counter+1),
				@createdBy,
				GETDATE(),
				@createdBy,
				GETDATE()
			)

			SET @Counter = @Counter + 1
		END
END

SET @selectAccType = @typeID1;--postavljamo da je ID za select tipa prvi uneseni tip iz baze
SET @selectCustomerId = @customerId;--postavljamo da je ID korisnika za select poslednji uneseni korisnik

SELECT
       CONCAT(ce.FirstName, ' ',ce.LastName) AS 'Vlasnik racuna',
       at.Type,ac.Number AS 'Broj racuna',
       SUM(t.Value) AS 'Stanje na racunu'
FROM Bank.Accounts ac
JOIN Bank.Customers ce
       ON ac.CustomerId = ce.Id
JOIN Bank.Transactions t
       ON ac.Id = t.AccountId
JOIN Bank.AccountTypes at 
       ON at.id = ac.AccountTypeId
GROUP BY ce.FirstName, ce.LastName, at.Type, ac.Number
/*SELECT ac.Number AS 'Broj racuna', CONCAT(ce.FirstName, ' ',ce.LastName) AS 'Vlasnik racuna', SUM(t.Value) AS 'Stanje na racunu'
FROM Bank.Accounts ac
JOIN Bank.Customers ce ON ac.CustomerId = ce.Id
JOIN Bank.Transactions t ON ac.Id = t.AccountId
WHERE ce.Id= @selectCustomerId AND ac.AccountTypeId = @selectAccType
GROUP BY ce.FirstName, ce.LastName, ac.Number
ORDER BY ce.LastName ASC*/