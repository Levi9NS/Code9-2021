--DELETE FROM [Survey].[Answers]
--DELETE FROM Survey.Participants
--DELETE FROM Survey.QuestionOfferedAnswerRelations
--DELETE FROM [Survey].[OfferedAnswers]
--DELETE FROM [Survey].[SurveyQuestionRelations]
--DELETE FROM [Survey].[Questions]
--DELETE FROM [Survey].[GeneralInformations]

DECLARE @surveyId INT,
        @question1Id INT,
		@question1Text VARCHAR(200) = 'Are you satisfied with complexity of homework?',
		@question2Id INT,
		@question2Text VARCHAR(200) = 'Are you satisfied with length of presentation?',
		@question3Id INT,
		@question3Text VARCHAR(200) = 'Are you satisfied content of presentation?',
		@OfferedAnswersId1 INT,
		@OfferedAnswers1Text VARCHAR(100) = 'Yes',
		@OfferedAnswersId2 INT,
		@OfferedAnswers2Text VARCHAR(100) = 'No',
		@OfferedAnswersId3 INT,
		@OfferedAnswers3Text VARCHAR(100) = 'Maybe',
		@OfferedAnswersId4 INT,
		@OfferedAnswers4Text VARCHAR(100) = 'Other',
		@OfferedAnswersIdRandom INT,
		@createdBy VARCHAR(50) = 'Vasilije',
		@numberOfParticipients INT = 100,
		@Counter INT = 0,
		@participentId INT,
		@surveyName VARCHAR(50) = 'Angular Web APP'
		--@surveyName VARCHAR(50) = '.NET Core Web API including SOLID and Design Patterns'
		--@surveyName VARCHAR(50) = 'Azure Database (SQL and Cosmos)'

IF NOT EXISTS (SELECT 1 FROM Survey.GeneralInformations WHERE Description = @surveyName)
BEGIN
	INSERT INTO Survey.GeneralInformations
	( 
		Description,
		StartDate,
		EndDate,
		IsOpen,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   
		@surveyName,
		GETDATE(), -- StartDate - datetime
		(DATEADD(DAY, 5, GETDATE())), -- EndDate - datetime
		1,      -- IsOpen - bit
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @surveyId = SCOPE_IDENTITY()


	INSERT INTO Survey.Questions (QuestionText,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @question1Text,        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)
	SET @question1Id = SCOPE_IDENTITY()

	INSERT INTO Survey.Questions (QuestionText,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @question2Text,        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)
	SET @question2Id = SCOPE_IDENTITY()

	INSERT INTO Survey.Questions (QuestionText,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @question3Text,        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
	)
	SET @question3Id = SCOPE_IDENTITY()


	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question1Id,         -- QuestionId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question2Id,         -- QuestionId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question3Id,         -- QuestionId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)


	INSERT INTO Survey.OfferedAnswers
	(
		Text,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @OfferedAnswers1Text,         -- Text - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @OfferedAnswersId1 = SCOPE_IDENTITY()

	INSERT INTO Survey.OfferedAnswers
	(
		Text,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @OfferedAnswers2Text,         -- Text - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @OfferedAnswersId2 = SCOPE_IDENTITY()

	INSERT INTO Survey.OfferedAnswers
	(
	    Text,
	    ChangedBy,
	    ChangedDate,
	    CreatedBy,
	    CreateDate
	)
	VALUES
	(   @OfferedAnswers3Text,        -- Text - varchar(200)
	    @createdBy,        -- ChangedBy - varchar(50)
	    GETDATE(), -- ChangedDate - datetime
	    @createdBy,        -- CreatedBy - varchar(50)
	    GETDATE()  -- CreateDate - datetime
	    )

	SET @OfferedAnswersId3 = SCOPE_IDENTITY()

	INSERT INTO Survey.OfferedAnswers
	(
	    Text,
	    ChangedBy,
	    ChangedDate,
	    CreatedBy,
	    CreateDate
	)
	VALUES
	(   @OfferedAnswers4Text,        -- Text - varchar(200)
	    @createdBy,        -- ChangedBy - varchar(50)
	    GETDATE(), -- ChangedDate - datetime
	    @createdBy,        -- CreatedBy - varchar(50)
	    GETDATE()  -- CreateDate - datetime
	    )

	SET @OfferedAnswersId4 = SCOPE_IDENTITY()

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question1Id,         -- QuestionId - int
		@OfferedAnswersId1,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question1Id,         -- QuestionId - int
		@OfferedAnswersId2,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question1Id,         -- QuestionId - int
		@OfferedAnswersId3,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question1Id,         -- QuestionId - int
		@OfferedAnswersId4,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question2Id,         -- QuestionId - int
		@OfferedAnswersId1,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question2Id,         -- QuestionId - int
		@OfferedAnswersId2,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)


	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question3Id,         -- QuestionId - int
		@OfferedAnswersId1,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.QuestionOfferedAnswerRelations
	(
		QuestionId,
		OfferedAnswerId,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   @question3Id,         -- QuestionId - int
		@OfferedAnswersId2,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

WHILE ( @Counter <= @numberOfParticipients - 1)
	BEGIN
		INSERT INTO Survey.Participants( SurveyId,FirstName,LastName,Email,Password,ChangedBy,ChangedDate,CreatedBy,CreateDate)
		VALUES
		(   @surveyId,         -- SurveyId - int
			'Name' + CONVERT(VARCHAR(10), @Counter),        -- FirstName - varchar(50)
			'SureName' + CONVERT(VARCHAR(10), @Counter),        -- LastName - varchar(50)
			'Email' + CONVERT(VARCHAR(10), @Counter),        -- Email - varchar(50)
			'zCvF/OSvj1ga+r4pjKtiDn/ZUv8=',        -- Password - varchar(50)
			@createdBy,        -- ChangedBy - varchar(50)
			GETDATE(), -- ChangedDate - datetime
			@createdBy,        -- CreatedBy - varchar(50)
			GETDATE()  -- CreateDate - datetime
			)

		SET @participentId = SCOPE_IDENTITY()

		SELECT @OfferedAnswersIdRandom = id
		FROM Survey.OfferedAnswers
		ORDER BY NEWID()

		INSERT INTO Survey.Answers
		(
			ParticipantId,
			SurveyId,
			QuestionId,
			QuestionAnswersId,
			ChangedBy,
			ChangedDate,
			CreatedBy,
			CreateDate
		)
		VALUES
		(   @participentId,         -- ParticipantId - int
			@surveyId,         -- SurveyId - int
			@question1Id,         -- QuestionId - int
			@OfferedAnswersIdRandom,         -- QuestionAnswersId - int
			@createdBy,        -- ChangedBy - varchar(50)
			GETDATE(), -- ChangedDate - datetime
			@createdBy,        -- CreatedBy - varchar(50)
			GETDATE()  -- CreateDate - datetime
			)

		SELECT @OfferedAnswersIdRandom = id
		FROM Survey.OfferedAnswers
		ORDER BY NEWID()

		INSERT INTO Survey.Answers
		(
			ParticipantId,
			SurveyId,
			QuestionId,
			QuestionAnswersId,
			ChangedBy,
			ChangedDate,
			CreatedBy,
			CreateDate
		)
		VALUES
		(   @participentId,         -- ParticipantId - int
			@surveyId,         -- SurveyId - int
			@question2Id,         -- QuestionId - int
			@OfferedAnswersIdRandom,         -- QuestionAnswersId - int
			@createdBy,        -- ChangedBy - varchar(50)
			GETDATE(), -- ChangedDate - datetime
			@createdBy,        -- CreatedBy - varchar(50)
			GETDATE()  -- CreateDate - datetime
			)

		SELECT @OfferedAnswersIdRandom = id
		FROM Survey.OfferedAnswers
		ORDER BY NEWID()

		INSERT INTO Survey.Answers
		(
			ParticipantId,
			SurveyId,
			QuestionId,
			QuestionAnswersId,
			ChangedBy,
			ChangedDate,
			CreatedBy,
			CreateDate
		)
		VALUES
		(   @participentId,         -- ParticipantId - int
			@surveyId,         -- SurveyId - int
			@question3Id,         -- QuestionId - int
			@OfferedAnswersIdRandom,         -- QuestionAnswersId - int
			@createdBy,        -- ChangedBy - varchar(50)
			GETDATE(), -- ChangedDate - datetime
			@createdBy,        -- CreatedBy - varchar(50)
			GETDATE()  -- CreateDate - datetime
			)

		SET @Counter = @Counter + 1
	END	
END

SELECT ge.Description,q.QuestionText, oa.text, COUNT(*) AS count
FROM 
	Survey.GeneralInformations ge
	INNER JOIN Survey.SurveyQuestionRelations sqr
		ON ge.id = sqr.SurveyId
	INNER JOIN Survey.Questions q
		ON sqr.QuestionId = q.id
	INNER JOIN Survey.QuestionOfferedAnswerRelations qoar
		ON q.id = qoar.QuestionId
	INNER JOIN Survey.OfferedAnswers oa
		ON qoar.OfferedAnswerId = oa.id
	INNER JOIN Survey.Participants pa
		ON pa.SurveyId = pa.SurveyId
	INNER JOIN Survey.Answers a
		ON pa.id = a.ParticipantId AND qoar.QuestionId = a.QuestionId AND qoar.OfferedAnswerId = a.QuestionAnswersId
GROUP BY ge.id,ge.Description,oa.id,q.id,q.QuestionText,oa.Text
ORDER BY ge.id,q.id,oa.id



