import { OfferedAnswersModel } from "./answers-response";

export class SurveyResponse {
  public id: string;
  public name: string;
  public questions: Questions[];
  public startDate: string;
  public endDate: string;
}

export class Questions {
  public questionText: string;
  public id: string;
}

export class QuestionAndAnswers{
  text: string;
  answers: OfferedAnswersModel[];
  surveyId: number;
}

