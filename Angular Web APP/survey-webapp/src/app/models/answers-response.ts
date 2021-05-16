export class AnswersResponse {
  public name: string;
  public questions: Question[];
}

export class Question {
  public text: string;
  public response: string;
}
