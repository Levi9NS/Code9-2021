export class SurveyResponse {
  public id: string;
  public name: string;
  public startDate: string;
  public endDate: string;
  public questions: Questions[];
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

