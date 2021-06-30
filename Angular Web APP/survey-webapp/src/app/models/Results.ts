export class ResultModel {
    public title: string;
    public pages: PageResultModel[];
  }
  
  export class PageResultModel {
    public name: string;
    questions: Result[];
  }


export class Result 
{
   
    text: string;
    response:  string;
    count: number;
}