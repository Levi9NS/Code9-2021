export interface SubmitSurvey{
    participant:{
        surveyId:number,
        firstName: string,
        lastName:string,
        email:string,
        password:string
      },
      answers:Answers[]
}

export interface Answers{
    surveyId:number,
    questionId:number,
    questionAnswersId:number
}