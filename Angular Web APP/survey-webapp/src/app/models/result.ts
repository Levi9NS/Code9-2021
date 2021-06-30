export class Answers{
    SurveyId : number;
    ParticipantId: number;
    AnsweredQuestions: ParticipantAnswers[];
}

export class ParticipantAnswers{
    QuestionId: number;
    QuestionAnswersId: number;
}

export class ResultTemp{
    questionText: string;
    AnswerText: string;
    count: number;
}