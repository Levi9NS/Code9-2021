export class SurveyModel {
  public title: string;
  public pages: PageModel[];
}

export class PageModel {
  public name: string;
  questions: QuestionModel[];
}

export class QuestionModel {
  public name: string;
  public title: string;
  public type: string;
  public choices: string[];
  public isRequired: boolean;
  public hasOther: boolean;
  public visibleIf?: string;
}
