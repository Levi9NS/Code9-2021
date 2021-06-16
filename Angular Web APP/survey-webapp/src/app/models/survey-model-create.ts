export class SurveyModelCreate {
    public title: string;
    public pages: PageModel[];
  }
  
  export class PageModel {
    public name: string;
    elements: ElementModel[];
    public description: string;
  }
  
  export class ElementModel {
    public name: string;
    public title: string;
    public type: string;
    public choices: string[];
    public isRequired: boolean;
    public hasOther: boolean;
    public visibleIf?: string;
  }
  