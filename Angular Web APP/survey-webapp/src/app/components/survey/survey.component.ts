import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Model, StylesManager, SurveyNG, Survey, } from 'survey-angular';
import { OfferedAnswers } from '../../models/answers-response';
import { QuestionModel, SurveyModel } from '../../models/survey-model';
import { SurveyResponse } from '../../models/survey-response';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { SubmitSurvey,Answers } from '../../models/submit-survey';
import { Router } from '@angular/router';
@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})

export class SurveyComponent implements OnInit {
  private queryParamSurveyId = 'id';
  private surveyRootElementId = 'survey-element';
  private surveyId;
  survey: SurveyResponse;
  dataLoaded = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute,private router: Router) { }

  ngOnInit() {
     this.surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(this.surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(this.surveyId)
      .subscribe(answers => {
        const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
      });
    });
  }

  convertAPIDataToSurveyModel(survey: SurveyResponse, answers: OfferedAnswers): SurveyModel {
    const surveyModel = {
      id:survey.id,
      title: survey.name,
      pages: [{
        name: 'Survey questions',
        questions: []
      },
      {
        name: 'Participant information',
        questions: [
          {
            name: "firstName",
            type: "text",
            title: "Please enter your first name:",
            isRequired: true
          },
          {
            name: "lastName",
            type: "text",
            title: "Please enter your last name:",
            isRequired: true
          },
          {
            name: "email",
            type: "text",
            inputType: "email",
            title: "Please enter your email:",
            isRequired: true,
            placeHolder: "name@example.com",
            autoComplete: "email",
            validators: [
              {
                  type: "email"
              }
          ]
          },
          {
            name: "password",
            type: "text",
            inputType: "password",
            title: "Please enter your password:",
            isRequired: true
          }
        ]
        
      }]
    } as SurveyModel;

    surveyModel.pages[0].questions = survey.questions.map<QuestionModel>(question => {
      const result = answers.offeredAnswers.filter(answer => answer.questionId === question.id);
      const offeredAnswers = result.map(res => res.questionAnswer);
      return {
        id: question.id,
        name: question.id.toString(),
        title: question.questionText,
        choices: offeredAnswers.filter(res => res.toString().toLocaleLowerCase() !== 'other').map(r => r).sort(),
        isRequired: true,
        type: 'radiogroup',
        hasOther: offeredAnswers.findIndex(res => res.toString().toLocaleLowerCase() === 'other') !== -1
      };
    });

    return surveyModel;
  }

  sendDataToServer = (surveyResult, options) => {
    options.showDataSaving();
    const submit= this.prepereDataForServer(surveyResult.data);
    this.surveyService.submitSurvey(submit).subscribe(
      answer =>{
      options.showDataSavingSuccess();
      setTimeout(() => {
        this.router.navigate(['']);
      }, 2000);
    },error =>{
      options.showDataSavingError();
    } 
    )
  }

  prepereDataForServer(surveyResult):SubmitSurvey{
    var submit={
      answers:[
      ],
      participant:{
        surveyId:parseInt(this.surveyId),
        email:surveyResult['email'],
        firstName:surveyResult['firstName'],
        lastName:surveyResult['lastName'],
        password:surveyResult['password']
      }
    } as SubmitSurvey;

    for (var answer in surveyResult){
      if(answer!=="firstName" && answer!=="lastName" && answer!=="password" && answer!=="email"){
        const offeredAnswerId=this.getQuestionAnswersId(surveyResult[answer],answer);
        if(offeredAnswerId !==""){
          submit.answers.push({
            questionAnswersId: parseInt(offeredAnswerId),
            questionId: parseInt(answer),
            surveyId : parseInt(this.surveyId)
          })
        }
      }
    }

    return submit;
  };

  getQuestionAnswersId(answerText,questionId):string{
    var id = "";
    this.survey.questions.map(q=>{
      if(q.id==questionId){
        q.offeredAnswers.map(oa=>{
          if(oa.text.toLocaleLowerCase()===answerText.toLocaleLowerCase()){
            id = oa.id;
          }
        })
      }
    })
    return id;
  };

 
}
