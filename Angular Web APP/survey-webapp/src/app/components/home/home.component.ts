import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ShortSurveyModel } from 'src/app/models/survey-button-model';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  surveys = Array<ShortSurveyModel>();
  dataLoaded = false;

  constructor(private service: SurveyService, private router: Router) { }

  ngOnInit() {
    // get surveys
    this.service.getDetailsOfSurveys().subscribe(
      res => {
        this.surveys = res as Array<ShortSurveyModel>;
        this.dataLoaded = true;
        this.surveys.forEach((item) => {
          item['isAvailable'] = 0;
          var now = new Date();
          var surveyDate = new Date(item['endDate']);
          if (now > surveyDate){
            item['isAvailable'] = 1;
          }
        })
      },
      err => {
        console.log(err);
      }
    );
  }

  goToSurvey(id: number)
  {
    this.router.navigateByUrl('/' + id.toString());
  }

  results(id: number)
  {
    this.router.navigateByUrl('/' + id.toString() + '/result');
  }

  viewQuestions(id: number)
  {
    this.router.navigateByUrl('/' + id.toString() + '/questions');
  }

  allQuestions()
  {
    this.router.navigateByUrl('/survey/getAllQuestions');
  }

  allOfferedAnswers()
  {
    this.router.navigateByUrl('/survey/getAllOfferedAnswers');
  }

  addNewSurvey()
  {
    this.router.navigateByUrl('/survey/add');
  }
}
