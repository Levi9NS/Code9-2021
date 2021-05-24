import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionAnswer, SurveyResult } from 'src/app/models/survey-result';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Model, StylesManager, SurveyNG } from 'survey-angular';
import { OfferedAnswers } from '../../models/answers-response';
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
  offeredAnswers: OfferedAnswers;
  dataLoaded = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute,
              private notificationService: NotificationService) { }

  ngOnInit() {
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(surveyId)
      .subscribe(answers => {
        this.offeredAnswers = answers;
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
        type: 'radiogroup',
        hasOther: offeredAnswers.findIndex(res => res.toLocaleLowerCase() === 'other') !== -1
      };
    });

    return surveyModel;
  }

  sendDataToServer = (surveyResult) => {
    const result = new SurveyResult(this.survey.id, 4);
    result.Answers = [];

    Object.keys(surveyResult.data).forEach(key => {
      const answeredQuestion = this.survey.questions.find(question => question.questionText === key);
      const chosenAnswer = this.offeredAnswers.offeredAnswers.find(
        answer => answer.questionId === answeredQuestion.id && answer.questionAnswer === surveyResult.data[key]);
      if (answeredQuestion && chosenAnswer) {
        result.Answers.push({
          QuestionAnswerId: chosenAnswer.answerId,
          QuestionId: answeredQuestion.id
        });
      }
    });

    this.surveyService.postSurveyResult(result).subscribe(res => {
      this.notificationService.successMessage('Results are saved');
    }, error => {
      this.notificationService.errorMessage('Failed to save results');
    });
    console.log(surveyResult.data);
    console.log({result});
  }
}
