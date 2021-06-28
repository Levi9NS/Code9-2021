export class SurveyResults
{
    name : string;
    questions: AnswerdQuestions[];
}

export class AnswerdQuestions
{
    text : string
    response : string;
    count : number;
    id : number;
}