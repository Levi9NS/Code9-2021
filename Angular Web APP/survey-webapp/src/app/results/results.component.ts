import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Model, SurveyNG } from 'survey-angular';
import { PageResultModel, Result, ResultModel } from '../models/Results';
import { SurveyService } from '../services/survey-service/survey-service.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit {

  surveyid: string='';
  results: PageResultModel;
  private queryParamSurveyId = 'id';
  private surveyRootElementId = 'survey-element';
  dataLoaded = false;
  constructor(private surveyService: SurveyService,private route:ActivatedRoute, private router: Router) { }
 
    ngOnInit() {
     
      const surveyId = this.route.snapshot.params[this.queryParamSurveyId];
  
      this.surveyService.getSurveyResults(surveyId)
      .subscribe(surveyResponse => {
        console.log(surveyResponse);
        this.results=surveyResponse;
       
          const surveyModel = this.convertAPIDataToSurveyModel(this.results);
          const survey = new Model(surveyModel);
         
          SurveyNG.render(this.surveyRootElementId, {model: survey});
          
          this.dataLoaded = true;
        });
   
    }
  
    convertAPIDataToSurveyModel(result: PageResultModel) : ResultModel {
      const surveyModel = {
        title: "Results for " +result.name,
        pages: [{
        
          name: 'page1',
          questions: []
        }]
      } as ResultModel;
      surveyModel.pages[0].name=result.name;
      surveyModel.pages[0].questions=[];
      surveyModel.pages[0].questions = result.questions.map<Result>(r => {
  
       
        return {
          type: "html",
          text: r.text,
          response:r.response,
          count:r.count,
          "html": "<label>"+r.text+"</label>\n    <label>"+r.response+"</label>\n    <label>"+r.count+"</label>\n  </tr>\n  ",
         
         
        };
      });
      console.log(surveyModel);
      return surveyModel;
    }
  
    

}
