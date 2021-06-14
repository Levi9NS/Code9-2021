import { Component, OnInit } from '@angular/core';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { SurveyResponse } from '../../models/survey-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public surveyList : SurveyResponse[];
  dataLoaded = false;
  constructor(private readonly surveyService: SurveyService) { }

  ngOnInit() {
    this.surveyService.getAllSurveys().subscribe(answr => {
      this.surveyList=answr;
      this.dataLoaded = true;
    })
  }

  deleteSurvey(survey : SurveyResponse){
    if(confirm("Are you sure to delete "+survey.name)) {
      this.surveyService.deleteSurvey(survey.id).subscribe(answer => {
        this.surveyList = this.surveyList.filter(item => item !== survey);
      });
    }
  }

}
