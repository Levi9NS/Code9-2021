export interface SurveyResult{
    name : string;
    questions : QuestionsResult[];
    participantName:string;

}

export interface QuestionsResult{
    questionText:string;
    questionId:string;
    answerResult:AnswerResult[];
}

export interface AnswerResult{
    answer:string;
    count:number;
}