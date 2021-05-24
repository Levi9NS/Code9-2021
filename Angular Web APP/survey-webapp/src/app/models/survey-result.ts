export class SurveyResult {
    SurveyId: number;
    ParticipantId: number;
    Answers: QuestionAnswer[];

    constructor(surveyId: number, participantId: number) {
        this.SurveyId = surveyId;
        this.ParticipantId = participantId;
    }
}

export class QuestionAnswer {
    QuestionId: number;
    QuestionAnswerId: number;
}
