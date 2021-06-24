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
  public offeredAnswers : OfferedAnswer[]
}

export class OfferedAnswer{
  public id: string;
  public text:string;
}