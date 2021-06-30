import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.css']
})
export class SurveyResultsComponent implements OnInit {

  surveyResults: any;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute, private spinner: NgxSpinnerService) { }

  async ngOnInit() {
    this.spinner.show();
    const surveyId = parseInt(this.route.snapshot.params['id']);
    this.surveyResults = await this.surveyService.getSurveyResults(surveyId);
    this.spinner.hide();
  }

}
