import { SurveyParticipant } from "./survey-participant";

export class SurveyAdd {
    public participant: SurveyParticipant;
    public answers: SurveyAnswer[];
}

export class SurveyAnswer {
    public surveyId: number;
    public questionId: number;
    public questionAnswersId: number;
}