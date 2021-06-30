import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Participant } from 'src/app/models/participant-model';
import { Answers, ParticipantAnswers } from 'src/app/models/result';
import { Model, StylesManager, SurveyNG } from 'survey-angular';
import { OfferedAnswer, OfferedAnswers, OfferedAnswersModel } from '../../models/answers-response';
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
  id = 0;
  participant: Participant;
  offeredAnswers: Array<OfferedAnswer> =[];
  answers: Answers;
  dataLoaded = false;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit() {
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];
    this.id = history.state.id;
    this.answers = new Answers();

    this.surveyService.getParticipant().subscribe(
      result=>{
        this.participant = result;
        console.log('Success!', this.participant);
      }
    );

    this.surveyService.getSurvey(surveyId)
      .subscribe(surveyResponse => {
        this.survey = surveyResponse;
        this.surveyService.getSurveyAnswers(surveyId)
          .subscribe(answers => {
            
            const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
            this.offeredAnswers = answers.offeredAnswers;
            console.log(this.offeredAnswers);
            const survey = new Model(surveyModel);
            survey.onComplete.add(this.sendDataToServer);
            SurveyNG.render(this.surveyRootElementId, { model: survey });
            this.dataLoaded = true;
          });
      });
  }

  convertAPIDataToSurveyModel(survey: SurveyResponse, answers: OfferedAnswers): SurveyModel {
    const surveyModel = {
      title: survey.name,
      pages: [
        {
          name: 'Answer Survey Questions',
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
    this.answers.SurveyId = this.id;
    this.answers.ParticipantId = this.participant.id;
    let answersHelper : ParticipantAnswers[] = [];
    console.log(surveyResult.data);
    this.survey.questions.map(question=>{
      var surveyQuestions = surveyResult.data[question.questionText] as Array<string>;
      console.log('surveyQuestions',surveyQuestions);
      console.log('surveyresultdata[]', surveyResult.data[question.questionText])
      for (let i = 0; i < surveyQuestions.length; i++)
      {
        this.offeredAnswers.forEach(answer => {
          if(answer.questionAnswer == surveyQuestions[i] && answer.questionId == question.id)
          {
            answersHelper.push(
            {
              QuestionId : parseInt(question.id, 10),
              QuestionAnswersId : answer.answerId
            }  
            )
          }
        })
      }
    })
    this.answers.AnsweredQuestions = answersHelper;
    console.log(this.answers);
    this.surveyService.addResult(this.answers).subscribe(
      result => {
        console.log(this.answers);
      }
    );
  }
}
