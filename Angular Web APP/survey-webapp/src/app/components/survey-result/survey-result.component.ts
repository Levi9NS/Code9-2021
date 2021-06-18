import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShortSurveyModel } from 'src/app/models/survey-button-model';
import { SurveyResultModel } from 'src/app/models/survey-result-model';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-survey-result',
  templateUrl: './survey-result.component.html',
  styleUrls: ['./survey-result.component.css']
})
export class SurveyResultComponent implements OnInit {

  surveyId;
  survey = new ShortSurveyModel();
  surveyResult = new SurveyResultModel();
  dataLoaded = false;
  dataExists = false;

  constructor(private service: SurveyService, private route: ActivatedRoute) { 
    route.params.subscribe(params => {this.surveyId = params['id'];});
  }

  ngOnInit() {
    this.service.getSurveyResults(this.surveyId).subscribe(
      res => {
        this.surveyResult = res as SurveyResultModel;
        if (this.surveyResult.questions.length != 0)
          {
            this.dataExists = true;
          }
        this.service.getSurveyDetails(this.surveyId).subscribe(
          res => {
            this.survey = res as ShortSurveyModel;
          },
          err =>
          {
            console.log(err);
          }
        )
        this.dataLoaded = true;
      },
      err => {
        console.log(err);
      }
    );
  }

}
