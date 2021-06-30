import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-initial-page',
  templateUrl: './initial-page.component.html',
  styleUrls: ['./initial-page.component.css']
})
export class InitialPageComponent implements OnInit {

  allSurveys: any;
  dataLoaded = false;

  constructor(private surveyService: SurveyService) { }

  async ngOnInit() {
    this.allSurveys = await this.surveyService.getAllSurveys();
    this.dataLoaded = true;
  }

  async closeSurvey(surveyId: number) {
    this.dataLoaded = false;
    await this.surveyService.closeSurvey(surveyId);
    this.allSurveys = await this.surveyService.getAllSurveys();
    this.dataLoaded = true;
  }

}
