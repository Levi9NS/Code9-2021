export class Answers{
    SurveyId : number;
    ParticipantId: number;
    AnsweredQuestions: ParticipantAnswers[];
}

export class ParticipantAnswers{
    QuestionId: number;
    QuestionAnswersId: number;
}
