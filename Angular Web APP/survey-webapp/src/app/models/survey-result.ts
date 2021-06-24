export class SurveyResult {
    public name : string;
    public questions : AnsweredQuestion[];
}

export class AnsweredQuestion {
    public text:string;
    public responses:AnsweredQuestionList[];
    public id:number;
}

export class AnsweredQuestionList {
    public response:string;
    public count:number;
}