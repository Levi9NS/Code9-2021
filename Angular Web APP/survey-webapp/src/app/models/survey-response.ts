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

