import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-initial-page',
  templateUrl: './initial-page.component.html',
  styleUrls: ['./initial-page.component.css']
})
export class InitialPageComponent implements OnInit {

  allSurveys: any;

  constructor(private surveyService: SurveyService, private spinner: NgxSpinnerService) { }

  async ngOnInit() {
    this.spinner.show();
    this.allSurveys = await this.surveyService.getAllSurveys();
    this.spinner.hide();
  }

  async closeSurvey(surveyId: number) {
    if (confirm("Are you sure you want to close this survey?")) {
      this.spinner.show();
      await this.surveyService.closeSurvey(surveyId);
      this.allSurveys = await this.surveyService.getAllSurveys();
      this.spinner.hide();
    }
  }

}
