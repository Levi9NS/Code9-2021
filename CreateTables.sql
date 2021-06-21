--DROP TABLE [Bank].[AccountsTransactionsRelations]
--DROP TABLE [Bank].[CustomersAccountsRelations]
--DROP TABLE [Bank].[Transactions]
--DROP TABLE [Bank].[Accounts]
--DROP TABLE [Bank].[Customers]

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
		[id] [int] identity(1,1) not null,
		[FirstName] varchar(max) not null,
		[LastName] varchar(max) not null,
		[JMBG] varchar(13) not null unique,
		[City] varchar(max) not null,
		[Postal] int not null,
		[Address] varchar(max) not null,
		[HouseNumber] int not null,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	constraint [PK_Customers] primary key clustered
	(
		[id] ASC
	)
	WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END

if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'Bank' and TABLE_NAME = 'Accounts')
begin
	create table [Bank].[Accounts](
		[id] [INT] identity(1,1) not null,
		[Description] varchar(max) not null,
		[Number] varchar(16) not null unique,
		[Balance] float not null,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
end

if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'Bank' and TABLE_NAME = 'CustomersAccountsRelations')
begin
	create table [Bank].[CustomersAccountsRelations](
		[id] [INT] identity(1,1) not null,
		[CustomerId] int not null,
		[AccountId] int not null,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	constraint [PK_CustomersAccountsRelations] primary key clustered
	(
		[id] asc
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	alter table [Bank].[CustomersAccountsRelations] with check add constraint [FK_CustomersAccountsRelations_CustomerId] foreign key([CustomerId])
	references [Bank].[Customers] ([Id])

	alter table [Bank].[CustomersAccountsRelations] with check add constraint [FK_CustomersAccountsRelations_AccountId] foreign key([AccountId])
	references [Bank].[Accounts] ([Id])
end

if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'Bank' and TABLE_NAME = 'Transactions')
begin
	create table [Bank].[Transactions](
		[id] [int] identity(1,1) not null,
		[Amount] float not null,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	CONSTRAINT [PK_OfferedAnswers] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) on [primary]
end

if not exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = 'Bank' and TABLE_NAME = 'AccountsTransactionsRelations')
begin
	create table [Bank].[AccountsTransactionsRelations](
		[id] [INT] identity(1,1) not null,
		[AccountId] int not null,
		[TransactionId] int not null,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	constraint [PK_AccountsTransactionsRelations] primary key clustered
	(
		[id] asc
	) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	alter table [Bank].[AccountsTransactionsRelations] with check add constraint [FK_AccountsTransactionsRelations_AccountId] foreign key([AccountId])
	references [Bank].[Accounts] ([Id])

	alter table [Bank].[AccountsTransactionsRelations] with check add constraint [FK_AccountsTransactionsRelations_TransactionId] foreign key([TransactionId])
	references [Bank].[Transactions] ([Id])
end