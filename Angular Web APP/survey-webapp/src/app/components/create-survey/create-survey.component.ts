import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { OfferedAnswers } from 'src/app/models/answers-response';
import { OfferedAnswer, Question, Survey } from 'src/app/models/survey';
import { SurveyResponse } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { Model, SurveyNG } from 'survey-angular';

@Component({
  selector: 'app-create-survey',
  templateUrl: './create-survey.component.html',
  styleUrls: ['./create-survey.component.css']
})
export class CreateSurveyComponent implements OnInit {

private surveyRootElementId = 'create-survey';
dataLoaded = false;
createSurvey: Survey;


constructor(private readonly surveyService: SurveyService,private router: Router) { }

ngOnInit() {
  
      const surveyModel = this.convertAPIDataToSurveyModel();
      const survey = new Model(surveyModel);
      survey.onComplete.add(this.sendDataToServer);
      SurveyNG.render(this.surveyRootElementId, {model: survey});
      this.dataLoaded = true;

}

convertAPIDataToSurveyModel(): SurveyResponse {

  var json = {
    "progressBarType": "buttons",
    "showProgressBar": "top",
    "title": "Create survey",
    "pages": [{
      "navigationTitle": "Basic Informations",
      "elements": [{
        "type": "text",
        "name": "titlesurvey",
        "isRequired": true
      },
      {
        "type": "text",
        "inputType": "date",
        "isRequired": true,
        "name": "startDate",
        "title": "Start date:"
      },
      {
        "type": "text",
        "inputType": "date",
        "isRequired": true,
        "name": "endDate",
        "title": "End date:"
      }]
    },
    {
      "navigationTitle": "Questions",
      "elements": [
        {
            "type": "matrixdynamic",
            "minRowCount": 1,
            "rowCount": 1,
            "name": "questions",
            "valueName": "questions",
            "isRequired": true,
            "title": "Please enter all your questions",
            "addRowText": "Add another question",
            "columns": [
                {
                    "name": "questionText",
                    "isRequired": true,
                    "title": "Question",
                    "cellType": "text"
                }
            ]
        }
    ]},
    {
      "navigationTitle": "Anwers",
      "title": "Enter answers",
      "elements": [
          {
              "type": "paneldynamic",
              "renderMode": "list",
              "allowAddPanel": false,
              "allowRemovePanel": false,
              "name": "answers",
              "title": "Answers",
              "valueName": "questions",
              "templateTitle": "Employer name: {panel.questionText}",
              "templateElements": [
                  {
                    "type": "panel",
                   
                    "elements": [
                      {
                          "type": "matrixdynamic",
                          "minRowCount": 1,
                          "rowCount": 1,
                          "name": "offeredanswers",
                          "isRequired": true,
                          "title": "Please enter all your anwers",
                          "addRowText": "Add another answer",
                          "columns": [
                              {
                                  "name": "text",
                                  "isRequired": true,
                                  "title": "Answers",
                                  "cellType": "text"
                              }
                          ]
                      }
                  ]






                  }]
                }]


    }]
   
    
    

    
   } as unknown as SurveyResponse

  return json;
}

sendDataToServer = (surveyResult) => {
  this.createSurvey=new Survey();
  this.createSurvey.name=surveyResult.data["titlesurvey"];
  this.createSurvey.startDate=surveyResult.data["startDate"];
  this.createSurvey.endDate=surveyResult.data["endDate"];
  this.createSurvey.questions=[];
  this.createSurvey.questions=surveyResult.data["questions"];
  console.log(this.createSurvey);
  console.log(surveyResult.data);
  this.surveyService.AddSurvey(this.createSurvey).subscribe(
          result =>
          {
               console.log(result);
          });
          this.router.navigate(['/']);
 
  
};
}