import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { AnswerCountValidator, Model, Question, StylesManager, SurveyNG } from 'survey-angular';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-survey-result',
  templateUrl: './survey-result.component.html',
  styleUrls: ['./survey-result.component.css']
})
export class SurveyResultComponent implements OnInit {
  private surveyRootElementId = 'survey-element2';
 
  constructor(private readonly surveyService: SurveyService) { }
  dataLoaded = false;

  //Create page for showing results of survey

  //https://surver9storage.z13.web.core.windows.net/survey-result


  ngOnInit() {
    
    const surveyModel = this.convertAPIDataToSurveyModel();
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        this.dataLoaded = true;
 
  }

   convertAPIDataToSurveyModel() {


    const surveyModelResult = 
    {
      "pages": [
       {
        "name": "page1",
        "elements": [
         {
          "type": "text",
          "name": "id",
          "title": "id ankete"
         }
        ]
       },
      ]
     }

    return surveyModelResult;
  }

  sendDataToServer = (surveyResult) => {
  
    let idAnketa=surveyResult.data["id"];
    this.surveyService.getSurveyResult(idAnketa)
    .subscribe(surveyResult => {
     const surveyModelResult = 
     {
       "pages": [
        {
         "elements": 
         [
          {
           "type": "html",
           "html": "<table border=1\n  <tr>\n     <th>Pitanje</th>\n    <th>Response</th>\n    <th>Count</th>\n  </tr>\n  <tr>\n"
           + surveyResult.questions.map(question=>   { 
             return "<td>"+question.text+"</td>\n    <td>"+question.response+"</td>\n    <td>"+question.count+"</td>\n  </tr>\n  "}   )
            
          }
         ]
        }
       ]
      }
       const survey = new Model(surveyModelResult);
       survey.onComplete.add(this.sendDataToServer);
       SurveyNG.render(this.surveyRootElementId, {model: survey});
       this.dataLoaded = true;

    });

}


}
