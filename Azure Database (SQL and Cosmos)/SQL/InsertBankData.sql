DELETE FROM [Bank].[Transaction]
DELETE FROM [Bank].[Accounts]
DELETE FROM [Bank].[Customers]
DELETE FROM [Bank].[AccountType]


	BEGIN
	SET IDENTITY_INSERT [Bank].[AccountType] ON
	INSERT INTO Bank.AccountType
	( 
		id,
		AccountTypeName
	)
	VALUES
	(   
		1,
		'Ziro racun'
	)
		INSERT INTO Bank.AccountType
	( 
		id,
		AccountTypeName
	)
	VALUES
	(   
		2,
		'Depozit'
	)
		INSERT INTO Bank.AccountType
	( 
		id,
		AccountTypeName
	)
	VALUES
	(   
		4,
		'Pozajmica'
	)
	INSERT INTO Bank.AccountType ( 
		id,
		AccountTypeName
	)
	VALUES
	(   
		3,
		'Kredit'
	)
	SET IDENTITY_INSERT [Bank].[AccountType] OFF
	END

	BEGIN 
		SET IDENTITY_INSERT [Bank].[Customers] ON
		INSERT INTO Bank.Customers (Id, FirstName, LastName, Jmbg, City, PostalCode, HouseNumber) VALUES (1, 'Andrea', 'Sabo', '123456789012', 'San Francisko', 'Sd355', '13')
		INSERT INTO Bank.Customers (Id, FirstName, LastName, Jmbg, City, PostalCode, HouseNumber) VALUES (2, 'Vasilije', 'Mijatovic', '123456789013', 'Beograd', 'Sd355', '15')
		SET IDENTITY_INSERT [Bank].[Customers] OFF
	END

	BEGIN 
		SET IDENTITY_INSERT [Bank].[Accounts] ON
		INSERT INTO Bank.Accounts (Id, AccountNumber, AccountTypeId, CustomerJmbg) VALUES (1,1111111111111111, 1, '123456789012')
		INSERT INTO Bank.Accounts (Id, AccountNumber, AccountTypeId, CustomerJmbg) VALUES (2,1111111111111112, 2, '123456789012')
		INSERT INTO Bank.Accounts (Id, AccountNumber, AccountTypeId, CustomerJmbg) VALUES (3,1111111111111113, 3, '123456789012')
		INSERT INTO Bank.Accounts (Id, AccountNumber, AccountTypeId, CustomerJmbg) VALUES (4,1111111111111115, 3, '123456789013')
		SET IDENTITY_INSERT [Bank].[Accounts] OFF
	END

		BEGIN 
		SET IDENTITY_INSERT [Bank].[Transaction] ON
		INSERT INTO [Bank].[Transaction] (Id, AccountId, Valuee, PositiveNegative) VALUES (1,1, 100, '+')
		INSERT INTO [Bank].[Transaction]  (Id, AccountId, Valuee, PositiveNegative) VALUES (2,1, 100, '+')
		INSERT INTO [Bank].[Transaction]  (Id, AccountId, Valuee, PositiveNegative) VALUES (3,1, 100, '+')
		INSERT INTO [Bank].[Transaction]  (Id, AccountId, Valuee, PositiveNegative) VALUES (4,1, 100, '+')
		INSERT INTO [Bank].[Transaction]  (Id, AccountId, Valuee, PositiveNegative) VALUES (5,1, -13, '-')



		SET IDENTITY_INSERT [Bank].[Transaction] OFF
	END