import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Question } from 'src/app/models/survey-response';
import { SurveyResults } from 'src/app/models/survey-result';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {
  errorMSG: string;
  dataloaded: boolean = true;
  surveyId: number;
  surveyResults= new SurveyResults();
  questions: Question[] = [];
  constructor(private service: SurveyService, private router: Router) { }

  ngOnInit() {
    this.surveyId = history.state.surveyId;

    this.service.getSurveyQuestions(this.surveyId).subscribe(
      result => {
        this.questions = result;
        console.log('qs', this.questions);
      },
      error => {
        console.error("Error Occured", error);
      }
    )

    this.service.getSurveyResults(this.surveyId).subscribe(
      result => {
        this.surveyResults = result;
        console.log('res',this.surveyResults);
      },
      error => {
        console.error("Error Occured", error);
      },
      () =>{
        console.log('res',this.surveyResults);
        if(this.surveyResults.name === null)
        {
          this.dataloaded = false;
        }
      }
    )
  }
 
  IsValid(i){
    if(this.surveyResults.questions[i] === undefined ||this.surveyResults.questions[i] === null ){
      console.log('undefined: ', this.surveyResults.questions[i] === undefined);
      console.log('null: ', this.surveyResults.questions[i] === null);
      return true;
    }
  }
  goBack(){
    this.router.navigateByUrl('');
  }
}
