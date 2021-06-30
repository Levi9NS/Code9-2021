import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.css']
})
export class SurveyResultsComponent implements OnInit {

  surveyResults: any;
  dataLoaded = false;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute) { }

  async ngOnInit() {
    const surveyId = parseInt(this.route.snapshot.params['id']);
    this.surveyResults = await this.surveyService.getSurveyResults(surveyId);
    this.dataLoaded = true;
  }

}
