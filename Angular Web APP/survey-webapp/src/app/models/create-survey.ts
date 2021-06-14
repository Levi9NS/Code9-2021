export class CreateSurvey{
    public name:string;
    public startDate:Date;
    public endDate:Date;
    public questions:CreateQuestion[];
}
export class CreateQuestion {
    public questionText: string;
    public offeredAnswers : CreateOfferedAnswer[]
}
export class CreateOfferedAnswer{
    public text:string;
}
