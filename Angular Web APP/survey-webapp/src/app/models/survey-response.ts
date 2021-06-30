import { OfferedAnswersModel } from "./answers-response";

export class SurveyResponse {
  public id: string;
  public name: string;
  public questions: Question[];
  public startDate: string;
  public endDate: string;
}

export class Question {
  public questionText: string;
  public id: string;
}

export class QuestionAndAnswers{
  QuestionText: string;
  SurveyId: number;
  Answers: string[];
}

