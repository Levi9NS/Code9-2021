export class Survey {
    public Name: string;
    public Questions: Questions[];
    public StartDate: string;
    public EndDate: string;
  }
  
  export class Questions {
    public QuestionText: string;
  }