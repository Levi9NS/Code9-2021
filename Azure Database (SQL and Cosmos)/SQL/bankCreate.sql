--DROP TABLE [Bank].[Customers]
--DROP TABLE [Bank].[AccountTypes]
--DROP TABLE [Bank].[Accounts]
--DROP TABLE [Bank].[Transactions]

--DROP SCHEMA Bank

IF NOT EXISTS (SELECT 1			  
			   FROM 
				INFORMATION_SCHEMA.SCHEMATA
			  WHERE 
				SCHEMA_NAME = 'Bank')
BEGIN
	EXEC ('CREATE SCHEMA Bank')
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Bank'
				AND TABLE_NAME = 'Customers')
BEGIN 
	CREATE TABLE [Bank].[Customers](
		[Id] int IDENTITY(1,1) NOT NULL,
		[JMBG] varchar(13) NOT NULL,
		[FirstName] varchar(20) NOT NULL,
		[LastName] varchar(20) NOT NULL,
		[City] varchar(20) NOT NULL,
		[PostalCode] varchar(10) NOT NULL,
		[Address] varchar(25) NOT NULL,
		[HouseNumber] varchar(9) NOT NULL,
		[ChangedBy] varchar(50) NULL,
		[ChangedDate] datetime NULL,
		[CreatedBy] varchar(50) NOT NULL,
		[CreateDate] datetime NOT NULL,
	 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
	 WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) 
	 ON [PRIMARY]
	 ALTER TABLE [Bank].[Customers] ADD CONSTRAINT [UN_Customer_JMBG] UNIQUE ([JMBG])
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Bank'
				AND TABLE_NAME = 'AccountTypes')
BEGIN 
	CREATE TABLE [Bank].[AccountTypes](
		[Id] int IDENTITY(1,1) NOT NULL,
		[Type] varchar(15) NOT NULL,
		[ChangedBy] varchar(50) NULL,
		[ChangedDate] datetime NULL,
		[CreatedBy] varchar(50) NOT NULL,
		[CreateDate] datetime NOT NULL,
	 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([Id] ASC)
	 WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
	 ON [PRIMARY]
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Bank'
				AND TABLE_NAME = 'Accounts')
BEGIN 
	CREATE TABLE [Bank].[Accounts](
		[Id] int IDENTITY(1,1) NOT NULL,
		[Number] varchar(16) NOT NULL,
		[CustomerId] int NOT NULL,
		[AccountTypeId] int NOT NULL,
		[ChangedBy] varchar(50) NULL,
		[ChangedDate] datetime NULL,
		[CreatedBy] varchar(50) NOT NULL,
		[CreateDate] datetime NOT NULL,
	 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC)
	 WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
	 ON [PRIMARY]

	ALTER TABLE [Bank].[Accounts] ADD CONSTRAINT [UN_Account_Number] UNIQUE ([Number])
	ALTER TABLE [Bank].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Account_CustomerId] FOREIGN KEY([CustomerId])
	REFERENCES [Bank].[Customers] ([Id])
	ALTER TABLE [Bank].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountTypeId] FOREIGN KEY([AccountTypeId])
	REFERENCES [Bank].[AccountTypes] ([Id])
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Bank'
				AND TABLE_NAME = 'Transactions')
BEGIN 
	CREATE TABLE [Bank].[Transactions](
		[Id] int IDENTITY(1,1) NOT NULL,
		[AccountId] int NOT NULL,
		[Value] int NOT NULL,
		[ChangedBy] varchar(50) NULL,
		[ChangedDate] datetime NULL,
		[CreatedBy] varchar(50) NOT NULL,
		[CreateDate] datetime NOT NULL,
	 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC)
	 WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
	 ON [PRIMARY]

	ALTER TABLE [Bank].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_AccountId] FOREIGN KEY([AccountId])
	REFERENCES [Bank].[Accounts] ([Id])
END	