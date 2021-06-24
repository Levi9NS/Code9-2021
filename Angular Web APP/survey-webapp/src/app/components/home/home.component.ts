import { Component, OnInit } from '@angular/core';
import { SurveyResponse } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  surveys: SurveyResponse[];
  constructor(private readonly surveyService: SurveyService) { }

  ngOnInit() {
    this.surveyService.getAllSurveys().subscribe(s => {
      this.surveys = s;
    })
  }

}
