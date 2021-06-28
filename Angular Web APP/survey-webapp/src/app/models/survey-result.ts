export class SurveyResults
{
    name: string;
    questions: AnswerdQuestions[];
}

export class AnswerdQuestions
{
    question : string
    answer : string;
    count : number;
}