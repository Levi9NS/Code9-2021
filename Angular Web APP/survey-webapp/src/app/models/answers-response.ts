import { NumberValueAccessor } from "@angular/forms/src/directives";

export class OfferedAnswers {
  public offeredAnswers: OfferedAnswer[];
}

export class OfferedAnswer {
  public questionAnswer: string;
  public answerId:number;
  public questionId: string;
}

export class OfferedAnswersModel{
  id:number;
  text:string;
}
