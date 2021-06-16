import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Answer } from 'src/app/models/answer';
import { AnswerTemp } from 'src/app/models/answerTemp';
import { AnswerCountValidator, Model, StylesManager, SurveyNG } from 'survey-angular';
import { OfferedAnswer, OfferedAnswers } from '../../models/answers-response';
import { QuestionModel, SurveyModel } from '../../models/survey-model';
import { SurveyResponse } from '../../models/survey-response';
import { SurveyService } from '../../services/survey-service/survey-service.service';
@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})

export class SurveyComponent implements OnInit {
  private queryParamSurveyId = 'id';
  private surveyRootElementId = 'survey-element';
  survey: SurveyResponse;
  surveyAnswers:OfferedAnswers;
  dataLoaded = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

  //Adjusting functionality for saving survey result with participant data

  //https://surver9storage.z13.web.core.windows.net/survey/7

  //https://surver9storage.z13.web.core.windows.net/survey/2


  ngOnInit() {
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];
    this.surveyService.getSurvey(surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(surveyId)
      .subscribe(answers => {
        this.surveyAnswers=answers;
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
      title: survey.name,
      pages: [{
        name: 'page1',
        questions: []
      }]
    } as SurveyModel;

    surveyModel.pages[0].questions = survey.questions.map<QuestionModel>(question => {
      const result = answers.offeredAnswers.filter(answer => answer.questionId === question.id);
      const offeredAnswers = result.map(res => res.questionAnswer);
      return {
        name: question.questionText,
        title: question.questionText,
        choices: offeredAnswers.filter(res => res.toLocaleLowerCase() !== 'other').map(r => r).sort(),
        isRequired: true,
        type: 'checkbox',
        hasOther: offeredAnswers.findIndex(res => res.toLocaleLowerCase() === 'other') !== -1
      };
    });

    return surveyModel;
  }

  sendDataToServer = (surveyResult) => {
    var resultAsString = JSON.stringify(surveyResult.data);
    let answerForQuestionId=[];
    let answers=[];
    this.survey.questions.map(res=>{
      var answerTextData=surveyResult.data[res.questionText];
      const result=this.surveyAnswers.offeredAnswers.filter(answer=>answer.questionAnswer==answerTextData);
      answerForQuestionId.push(new AnswerTemp(result[0].answerId,result[0].questionId,result[0].questionAnswer));
    })
    answerForQuestionId.map(res=>{
      answers.push(new Answer(res.answerId,res.questionId,this.survey.id,207));  
    });
    answers.map(answer=>{
      this.surveyService.addSurveyResult(answer).subscribe(surveyResponse => {

      });

    });
    
  }
}
