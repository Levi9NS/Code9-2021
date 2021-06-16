export class SurveyResponse {
  public id: string;
  public name: string="aa";
  public questions:Array<Questions> = []
  public startDate: string;
  public endDate: string;

  get nameg(): string {
    return this.name;
  }
  set names(value: any) {
    this.name = value;
  }


}

export class Questions {
  public questionText: string;
  public id: string;

  get questionTextg(): string {
    return this.questionText;
  }
  set questionTexts(value: any) {
    this.questionText = value;
  }
} 

