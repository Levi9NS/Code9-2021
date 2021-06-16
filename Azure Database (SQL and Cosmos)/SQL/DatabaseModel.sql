--DROP TABLE [Survey].[Answers]
--DROP TABLE [Survey].[QuestionOfferedAnswerRelations]
--DROP TABLE [Survey].[OfferedAnswers]
--DROP TABLE [Survey].[SurveyQuestionRelations]
--DROP TABLE [Survey].[Questions]
--DROP TABLE [Survey].[Participants]
--DROP TABLE [Survey].[GeneralInformations]

--DROP SCHEMA Survey


IF NOT EXISTS (SELECT 1			  
			   FROM 
				INFORMATION_SCHEMA.SCHEMATA
			  WHERE 
				SCHEMA_NAME = 'Survey')
BEGIN
	EXEC ('CREATE SCHEMA Survey')
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'GeneralInformations')
BEGIN 
	CREATE TABLE [Survey].[GeneralInformations](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[Description] VARCHAR(MAX) NOT NULL,
		[StartDate] DATETIME NOT NULL,
		[EndDate] DATETIME NOT NULL,
		[IsOpen] BIT NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_GeneralInformations] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [Survey].[GeneralInformations] ADD  CONSTRAINT [DF_GeneralInformations_IsOpen]  DEFAULT ((0)) FOR [IsOpen]
END	


IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'Participants')
BEGIN 
	CREATE TABLE [Survey].[Participants](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[SurveyId] [INT] NOT NULL,
		[FirstName] [VARCHAR](50) NOT NULL,
		[LastName] [VARCHAR](50) NOT NULL,
		[Email] [VARCHAR](50) NOT NULL,
		[Password] [VARCHAR](50) NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_Participants] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [Survey].[Participants]  WITH CHECK ADD  CONSTRAINT [FK_Participants_SurveyId] FOREIGN KEY([SurveyId])
	REFERENCES [Survey].[GeneralInformations] ([Id])

END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'Questions')
BEGIN 
	CREATE TABLE [Survey].[Questions](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[QuestionText] VARCHAR(MAX) NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END	


IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'SurveyQuestionRelations')
BEGIN	
	CREATE TABLE [Survey].[SurveyQuestionRelations](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[SurveyId] INT NOT NULL,
		[QuestionId] INT NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_SurveyQuestionRelations] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [Survey].[SurveyQuestionRelations]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionRelations_SurveyId] FOREIGN KEY([SurveyId])
	REFERENCES [Survey].[GeneralInformations] ([Id])


	ALTER TABLE [Survey].[SurveyQuestionRelations]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionRelations_QuestionId] FOREIGN KEY([QuestionId])
	REFERENCES [Survey].[Questions] ([Id])
END	


IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'OfferedAnswers')
BEGIN	
	CREATE TABLE [Survey].[OfferedAnswers](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[Text] VARCHAR(200) NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_OfferedAnswers] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END	

IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'QuestionOfferedAnswerRelations')
BEGIN	
	CREATE TABLE [Survey].[QuestionOfferedAnswerRelations](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[QuestionId] INT NOT NULL,
		[OfferedAnswerId] INT NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_QuestionOfferedAnswerRelations] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

    ALTER TABLE [Survey].[QuestionOfferedAnswerRelations] WITH CHECK ADD  CONSTRAINT [FK_QuestionOfferedAnswerRelations_QuestionId] FOREIGN KEY([QuestionId])
	REFERENCES [Survey].[Questions] ([Id])

	ALTER TABLE [Survey].[QuestionOfferedAnswerRelations] WITH CHECK ADD  CONSTRAINT [FK_QuestionOfferedAnswerRelations_OfferedAnswerId] FOREIGN KEY([OfferedAnswerId])
	REFERENCES [Survey].[OfferedAnswers] ([Id])
END	


IF NOT EXISTS(SELECT 1
			  FROM 
				INFORMATION_SCHEMA.TABLES
			  WHERE 
				TABLE_SCHEMA = 'Survey'
				AND TABLE_NAME = 'Answers')
BEGIN	
	CREATE TABLE [Survey].[Answers](
		[id] [INT] IDENTITY(1,1) NOT NULL,
		[ParticipantId] [INT] NOT NULL,
		[SurveyId] [INT] NOT NULL,
		[QuestionId] [INT] NOT NULL,
		[QuestionAnswersId] [INT] NOT NULL,
		[ChangedBy] [VARCHAR](50) NULL,
		[ChangedDate] [DATETIME] NULL,
		[CreatedBy] [VARCHAR](50) NOT NULL,
		[CreateDate] [DATETIME] NOT NULL,
	 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [Survey].[Answers] WITH CHECK ADD  CONSTRAINT [FK_Answers_ParticipantId] FOREIGN KEY([ParticipantId])
	REFERENCES [Survey].[Participants] ([Id])

	ALTER TABLE [Survey].[Answers] WITH CHECK ADD  CONSTRAINT [FK_Answers_SurveyId] FOREIGN KEY([SurveyId])
	REFERENCES [Survey].[GeneralInformations] ([Id])
	
	ALTER TABLE [Survey].[Answers] WITH CHECK ADD  CONSTRAINT [FK_Answers_QuestionId] FOREIGN KEY([QuestionId])
	REFERENCES [Survey].[Questions] ([Id])

	ALTER TABLE [Survey].[Answers] WITH CHECK ADD  CONSTRAINT [FK_Answers_OfferedAnswersId] FOREIGN KEY([QuestionAnswersId])
	REFERENCES [Survey].[OfferedAnswers] ([Id])

	ALTER TABLE [Survey].[Answers] ADD CONSTRAINT [UN_Answers_ParticipantId_SurveyId_QuestionId_QuestionAnswersId] UNIQUE (ParticipantId,SurveyId,QuestionId,QuestionAnswersId)
END	


