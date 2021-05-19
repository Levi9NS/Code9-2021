import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Model, SurveyNG } from 'survey-angular';
import { AnswersResponse } from '../../models/answers-response';
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
  dataLoaded: boolean = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit() {
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(surveyId)
      .subscribe(answers => {
        const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
      });
    });
  }

  convertAPIDataToSurveyModel(survey: SurveyResponse, answers: AnswersResponse): SurveyModel {
    const surveyModel = {
      title: survey.name,
      pages: [{
        name: 'page1',
        questions: []
      }]
    } as SurveyModel;

    surveyModel.pages[0].questions = survey.questions.map<QuestionModel>(question => {
      const result = answers.questions.filter(answer => answer.text === question.questionText);
      const responses = result.map(res => res.response);
      return {
        name: question.questionText,
        title: question.questionText,
        choices: responses.filter(res => res.toLocaleLowerCase() !== 'other').map(r => r),
        isRequired: true,
        type: 'checkbox',
        hasOther: responses.findIndex(res => res.toLocaleLowerCase() === 'other') !== -1
      };
    });

    return surveyModel;
  }

  sendDataToServer = (surveyResult) => {
    console.log(surveyResult.data);
  }
}
