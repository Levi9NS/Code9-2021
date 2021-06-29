export class Survey {
    public Name: string;
    public StartDate: string;
    public EndDate: string;
    public Questions: QuestionWithAnswersOnly[] = [];
  }

  export class QuestionWithAnswersOnly{
    QuestionText : string;
    Answers: string[];
  }