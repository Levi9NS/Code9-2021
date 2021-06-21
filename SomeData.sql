--DELETE FROM [Bank].[AccountTransactionsRelations]
--DELETE FROM [Bank].[Transactions]
DELETE FROM [Bank].[Accounts]
--DELETE FROM [Bank].[CustomersAccountRelations]
DELETE FROM [Bank].[Customers]

DECLARE	@customerId INT,
		@customer1Id INT,
		@account1Id INT,
		@account2Id INT,
		@account3Id INT,
		@account4Id INT,
		@createdBy VARCHAR(50) = 'Vasilije'

begin
	insert into Bank.Customers
	(
		FirstName,
		LastName,
		JMBG,
		City,
		Postal,
		Address,
		HouseNumber,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	values
	(
		'Imenko',
		'Prezimic',
		'1111111111111',
		'Sremska Mitrovica',
		21000,
		'Neka ulica',
		16,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @customerId = SCOPE_IDENTITY()

	insert into Bank.Customers(FirstName,LastName,JMBG,City,Postal,Address,HouseNumber,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values
	(
		'Vasilije',
		'Najner',
		'1000010010010',
		'Novi Sad',
		22000,
		'Neka ulica',
		19,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @customer1Id = SCOPE_IDENTITY()

	insert into Bank.Accounts(Description,Number,Balance,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values
	(
		'Ziro Racun',
		'1601111111111172',
		10000.95,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @account1Id = SCOPE_IDENTITY()

	insert into Bank.Accounts(Description,Number,Balance,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values
	(
		'Depozit',
		'1610000000011120',
		100000.00,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @account2Id = SCOPE_IDENTITY()

	insert into Bank.Accounts(Description,Number,Balance,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values
	(
		'Pozajmica',
		'1001011011111120',
		0.00,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @account3Id = SCOPE_IDENTITY()

	insert into Bank.Accounts(Description,Number,Balance,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values
	(
		'Kredit',
		'1651011011111179',
		150000000.00,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	set @account4Id = SCOPE_IDENTITY()

	insert into Bank.CustomersAccountsRelations(CustomerId,AccountId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values(
		@customer1Id,
		@account1Id,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)

	insert into Bank.CustomersAccountsRelations(CustomerId,AccountId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values(
		@customer1Id,
		@account2Id,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)

	insert into Bank.CustomersAccountsRelations(CustomerId,AccountId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	values(
		@customerId,
		@account3Id,
		@createdBy,
		GETDATE(),
		@createdBy,
		GETDATE()
	)
	end