--DELETE FROM [Survey].[Answers]
--DELETE FROM Survey.Participants
--DELETE FROM Survey.QuestionOfferedAnswerRelations
--DELETE FROM [Survey].[OfferedAnswers]
--DELETE FROM [Survey].[SurveyQuestionRelations]
--DELETE FROM [Survey].[Questions]
--DELETE FROM [Survey].[GeneralInformations]



DECLARE @surveyId INT,
        @question1 INT,
		@question2 INT,
		@question3 INT,
		@OfferedAnswers1 INT,
		@OfferedAnswers2 INT,
		@createdBy VARCHAR(50) = 'Vasilije',
		@participentId1 INT,
		@participentId2 INT,
		@surveyName VARCHAR(50) = 'Angular Web APP'

IF NOT EXISTS (SELECT * FROM Survey.GeneralInformations WHERE Description = @surveyName)
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
	(   'Are you satisfied with length of survey?',        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)
	SET @question1 = SCOPE_IDENTITY()

	INSERT INTO Survey.Questions (QuestionText,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   'Are you satisfied with length of presenter?',        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)
	SET @question2 = SCOPE_IDENTITY()

	INSERT INTO Survey.Questions (QuestionText,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   'Are you satisfied with length of homework?',        -- Description - varchar(max)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
	)
	SET @question3 = SCOPE_IDENTITY()


	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question1,         -- QuestionId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question2,         -- QuestionId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.SurveyQuestionRelations (SurveyId,QuestionId,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		@question3,         -- QuestionId - int
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
	(   'Yes',         -- Text - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @OfferedAnswers1 = SCOPE_IDENTITY()

	INSERT INTO Survey.OfferedAnswers
	(
		Text,
		ChangedBy,
		ChangedDate,
		CreatedBy,
		CreateDate
	)
	VALUES
	(   'No',         -- Text - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @OfferedAnswers2 = SCOPE_IDENTITY()

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
	(   @question1,         -- QuestionId - int
		@OfferedAnswers1,         -- OfferedAnswerId - int
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
	(   @question1,         -- QuestionId - int
		@OfferedAnswers2,         -- OfferedAnswerId - int
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
	(   @question2,         -- QuestionId - int
		@OfferedAnswers1,         -- OfferedAnswerId - int
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
	(   @question2,         -- QuestionId - int
		@OfferedAnswers2,         -- OfferedAnswerId - int
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
	(   @question3,         -- QuestionId - int
		@OfferedAnswers1,         -- OfferedAnswerId - int
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
	(   @question3,         -- QuestionId - int
		@OfferedAnswers2,         -- OfferedAnswerId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	INSERT INTO Survey.Participants( SurveyId,FirstName,LastName,Email,Password,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		'Vasilije',        -- FirstName - varchar(50)
		'Gavric',        -- LastName - varchar(50)
		'v.gavric@levi9.com',        -- Email - varchar(50)
		'zCvF/OSvj1ga+r4pjKtiDn/ZUv8=',        -- Password - varchar(50)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @participentId1 = SCOPE_IDENTITY()

	INSERT INTO Survey.Participants( SurveyId,FirstName,LastName,Email,Password,ChangedBy,ChangedDate,CreatedBy,CreateDate)
	VALUES
	(   @surveyId,         -- SurveyId - int
		'Katarina',        -- FirstName - varchar(50)
		'Ivosevic',        -- LastName - varchar(50)
		'k.ivosevic@levi9.com',        -- Email - varchar(50)
		'zCvF/OSvj1ga+r4pjKtiDn/ZUv8=',        -- Password - varchar(50)
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

	SET @participentId2 = SCOPE_IDENTITY()

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
	(   @participentId1,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question1,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

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
	(   @participentId1,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question2,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)


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
	(   @participentId1,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question3,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)


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
	(   @participentId2,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question1,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

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
	(   @participentId2,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question2,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)

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
	(   @participentId2,         -- ParticipantId - int
		@surveyId,         -- SurveyId - int
		@question3,         -- QuestionId - int
		@OfferedAnswers2,         -- QuestionAnswersId - int
		@createdBy,        -- ChangedBy - varchar(50)
		GETDATE(), -- ChangedDate - datetime
		@createdBy,        -- CreatedBy - varchar(50)
		GETDATE()  -- CreateDate - datetime
		)
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
GROUP BY ge.Description,q.QuestionText,oa.Text






