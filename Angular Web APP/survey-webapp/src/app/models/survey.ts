export class Survey {
    public id: number;
    public name: string;
    public questions: Question[];
    public startDate: string;
    public endDate: string;
  }
  
  export class Question {
    public questionText: string;
     public offeredAnswers: OfferedAnswer[];
    public id: number;
  }

  export class OfferedAnswer {
    public text: string;
    public id: number;
  }