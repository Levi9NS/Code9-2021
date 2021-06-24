import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SurveyAdd } from 'src/app/models/survey-answer';
import { SurveyParticipant } from 'src/app/models/survey-participant';
import { Model, StylesManager, SurveyNG } from 'survey-angular';
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
  participantId: number;
  offeredAnswers: OfferedAnswer[];
  dataLoaded = false;
  addParticipantForm: FormGroup;
  surveyId: any;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.initializeForm();
    this.surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(this.surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(this.surveyId)
      .subscribe(answers => {
        const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
        const survey = new Model(surveyModel);
        this.offeredAnswers = answers.offeredAnswers;
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
      });
    });
  }

  initializeForm() {
    this.addParticipantForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(4)]),
      lastname: new FormControl('', [Validators.required, Validators.minLength(4)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(3)]),
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
        id: question.id,
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
    console.log(surveyResult.data);
    var body = {
      participant: {
        name: this.addParticipantForm.controls['name'].value,
        lastname: this.addParticipantForm.controls['lastname'].value,
        email: this.addParticipantForm.controls['email'].value,
        password: this.addParticipantForm.controls['password'].value
      },
      answers:[]
    } as SurveyAdd;
    this.survey.questions.forEach(q => {
      for (var answer in surveyResult.data) {
        if (q.questionText == answer) {
          const offeredAnswerId = this.offeredAnswers.forEach(oa => {
            if (oa.questionId == q.id && oa.questionAnswer==surveyResult.data[answer]) {
              body.answers.push({
                surveyId: this.surveyId,
                questionId: parseInt(q.id),
                questionAnswersId: parseInt(oa.oaId)
              });
            }
          });
      }
      }
    });
    this.surveyService.addSurveyResult(
      parseInt(this.surveyId),
      body.participant.name,
      body.participant.lastname,
      body.participant.email,
      body.participant.password,
      body.answers
      )
      .subscribe(err => { console.log(err); });
  }
}
