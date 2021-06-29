export class Survey {
    public Name: string;
    public StartDate: string;
    public EndDate: string;
    public Questions: Questions[];
  }

  export class Questions{
    QuestionText : string;
  }