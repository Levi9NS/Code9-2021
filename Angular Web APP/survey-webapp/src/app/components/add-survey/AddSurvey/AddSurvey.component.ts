import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Answer } from 'src/app/models/answer';
import { OfferedAnswers } from 'src/app/models/answers-response';
import { AnswerTemp } from 'src/app/models/answerTemp';
import { OfferedAnswerOriginal, OfferedAnswersOriginals } from 'src/app/models/offered-answer-original';
import { OfferedAnswerQuestionRelation } from 'src/app/models/offeredAnswerQuestionRelation';
import { QuestionModel, SurveyModel } from 'src/app/models/survey-model';
import { SurveyModelCreate } from 'src/app/models/survey-model-create';
import { Questions, SurveyResponse } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { AnswerCountValidator, Model, Question, StylesManager, SurveyNG } from 'survey-angular';


@Component({
  selector: 'app-AddSurvey',
  templateUrl: './AddSurvey.component.html',
  styleUrls: ['./AddSurvey.component.css']
})
export class AddSurveyComponent implements OnInit {


  private surveyRootElementId = 'survey-element1';
  offeredAnswers:OfferedAnswerOriginal[];
  dataLoaded = false;
//Create page adding general survey informations and general questions and answers

//surver9storage.z13.web.core.windows.net/add-survey

  constructor(private readonly surveyService: SurveyService) { }

  ngOnInit() {
    this.surveyService.getOfferedAnswers()
    .subscribe(offeredAnswersResponse => {
      this.offeredAnswers=offeredAnswersResponse;
        const surveyModel = this.convertAPIDataToSurveyModel();
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
    });
  }

  convertAPIDataToSurveyModel(): SurveyModelCreate {

    const surveyModelCreate = {
      title: "Anketa nova",
      "pages": [
       {
        "name": "page1",
        "elements": [
         {
          "type": "text",
          "name": "anketa",
          "title": "Unesi ime ankete"
         }
        ],
        "description": "Anketa 1"
       },
       {
        "name": "page2",
        "elements": [
         {
          "type": "text",
          "name": "pitanje",
          "title": "Unesi ime pitanja"
         },
         {
          "type": "checkbox",
          "name": "odgovori",
          "title": "Ponudjeni_odgovori",
          "choices":this.offeredAnswers.map(offered=>{

            return offered.text;
           
          
        }),
         
         }
        ]
       }
      ]
     }as SurveyModelCreate;

    return surveyModelCreate;
  }

  sendDataToServer = (surveyResult) => {

    let survey=new SurveyResponse();
    let question=new Questions();
 
    question.questionText=surveyResult.data["pitanje"];
    survey.name=surveyResult.data["anketa"];
    survey.questions.push(question);
   
   this.surveyService.addSurvey(survey).subscribe(surveyResponse => {
        surveyResult.data.odgovori.map(odgovor=>{
        let offeredQuestionAnswerRelation=new OfferedAnswerQuestionRelation();

        let result=this.offeredAnswers.filter(offeredAnswer=>offeredAnswer.text==odgovor);
        offeredQuestionAnswerRelation.questionId=surveyResponse.questions[0].id;

        offeredQuestionAnswerRelation.offeredAnswerId=result[0].id;
        this.surveyService.addOfferedAnswerQuestionRelation(offeredQuestionAnswerRelation).subscribe(offeredQuestionAnswerRelationResponse => {

        });
     });

    
  });
}
}