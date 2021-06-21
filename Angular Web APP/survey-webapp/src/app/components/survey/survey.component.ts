import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OfferedAnswer } from 'src/app/models/offered-answer';
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
  surveyId;
  survey: SurveyResponse;
  answers: OfferedAnswers;
  dataLoaded = false;
  allOfferedAnswers = new Array<OfferedAnswer>();

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) {
    route.params.subscribe(params => {this.surveyId = params['id'];});
   }

  ngOnInit() {
    
    this.surveyService.getSurvey(this.surveyId)
    .subscribe(surveyResponse => {
      this.survey = surveyResponse;
      this.surveyService.getSurveyAnswers(this.surveyId)
      .subscribe(answers => {
        const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
        this.answers = answers;
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
      });
    });

    this.surveyService.getAllOfferedAnswers().subscribe(
      res => {
        this.allOfferedAnswers = res as Array<OfferedAnswer>;
      },
      err => {
        console.log(err);
      }
    );
  }

  convertAPIDataToSurveyModel(survey: SurveyResponse, answers: OfferedAnswers): SurveyModel {
    const surveyModel = {
      title: survey.name,
      pages: [
      {
        name: 'page1',
        questions: []
      },
      {
        name: 'page2',
        questions: [
          {
            name: "firstName",
            type: "text",
            title: "First name:",
            isRequired: true
          },
          {
            name: "lastName",
            type: "text",
            title: "Last name:",
            isRequired: true
          },
          {
            name: "email",
            type: "text",
            inputType: "email",
            title: "Email:",
            isRequired: true,
            placeHolder: "name@example.com",
          },
          {
            name: "password",
            type: "text",
            inputType: "password",
            title: "Password:",
            isRequired: true
          }
        ]
      }
    ]
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
    var body = {
      SurveyId: parseInt(this.surveyId),
      Participant:{
        SurveyId: parseInt(this.surveyId),
        FirstName: surveyResult.data['firstName'],
        LirstName: surveyResult.data['lastName'],
        Email: surveyResult.data['email'],
        Password: surveyResult.data['password']
      },
      QuestionAnswers: []
    };

    this.survey.questions.map(q=>{
      // answer to a question surveyResult.data[q.questionText]
      var temp = surveyResult.data[q.questionText] as Array<string>;
      for (let i = 0; i<temp.length; i++)
      {
        this.allOfferedAnswers.forEach((item) => {
          if (item['text'] == temp[i])
          {
            body.QuestionAnswers.push({
            QuestionId: q.id,
            AnswerId: item['id']
            });
          }
        });
      }
    })
    this.surveyService.sendAnswer(body).subscribe(
      res =>{      
      }, err => {
        console.log(err);
      }
    )
  }
}
