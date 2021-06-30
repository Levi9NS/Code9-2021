import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Model, StylesManager, SurveyNG } from 'survey-angular';
import { OfferedAnswers } from '../../models/answers-response';
import { QuestionModel, SurveyModel } from '../../models/survey-model';
import { SurveyResponse } from '../../models/survey-response';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})

export class SurveyComponent implements OnInit {
  private queryParamSurveyId = 'id';
  private surveyRootElementId = 'survey-element';
  survey: SurveyResponse;
  dataLoaded = false;
  addParticipantForm: FormGroup;
  offeredAnswers: any;
  surveyId: any;
  allOfferedAnswers: any;
  participantFormError = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute, private spinner: NgxSpinnerService) { }

  async ngOnInit() {
    this.spinner.show();
    this.initForm();
    await this.initSurvey();
    this.spinner.hide();
  }

  initForm() {
    this.addParticipantForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  async initSurvey() {
    this.surveyId = this.route.snapshot.params[this.queryParamSurveyId];
    this.allOfferedAnswers = await this.surveyService.getOfferedAnswers();

    this.surveyService.getSurvey(this.surveyId)
      .subscribe(surveyResponse => {
        this.survey = surveyResponse;
        this.surveyService.getSurveyAnswers(this.surveyId)
          .subscribe(answers => {
            const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
            const survey = new Model(surveyModel);
            this.offeredAnswers = answers.offeredAnswers;
            survey.onComplete.add(this.sendDataToServer);
            SurveyNG.render(this.surveyRootElementId, { model: survey });
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

  sendDataToServer = async (surveyResult) => {
    if (this.addParticipantForm.valid) {
      this.spinner.show();
      this.participantFormError = false;
      const dataRequest = {
        participant: {
          firstName: this.addParticipantForm.controls['firstName'].value,
          lastName: this.addParticipantForm.controls['lastName'].value,
          email: this.addParticipantForm.controls['email'].value,
          password: this.addParticipantForm.controls['password'].value,
          surveyId: parseInt(this.surveyId),
        },
        answers: []
      }
      this.survey.questions.forEach(q => {
        for (let answer in surveyResult.data) {
          if (q.questionText == answer) {
            const questionAnsweredIds = surveyResult.data[answer].map(x => {
              const offeredAnswer = this.allOfferedAnswers.find(element => element.questionAnswer === x);
              return offeredAnswer.questionId;
            })
            dataRequest.answers.push({
              surveyId: parseInt(this.surveyId),
              questionId: parseInt(q.id),
              questionAnsweredIds: questionAnsweredIds
            })
          }
        }
      });

      await this.surveyService.addParticipantAnswers(dataRequest);
      this.spinner.hide();
    } else {
      await this.initSurvey();
      this.participantFormError = true;
    }
  }

}
