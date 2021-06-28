import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SurveyResults } from 'src/app/models/survey-result';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {
  dataloaded = false;
  surveyId: number;
  surveyResults= new SurveyResults();
  constructor(private service: SurveyService, private router: Router) { }

  ngOnInit() {
    this.surveyId = history.state.surveyId;
    this.service.getSurveyResults(this.surveyId).subscribe(
      result => {
        this.surveyResults = result;
        console.log(this.surveyResults);
      },
      error => {
        console.error("Error Occured", error);
      }
    );
    this.dataloaded = true;
  }

  goBack(){
    this.router.navigateByUrl('');
  }
}
