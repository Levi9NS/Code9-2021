import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OfferedAnswer } from 'src/app/models/offered-answer';
import { SurveyQuestion } from 'src/app/models/survey-question';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-offered-answers',
  templateUrl: './offered-answers.component.html',
  styleUrls: ['./offered-answers.component.css']
})
export class OfferedAnswersComponent implements OnInit {

  offeredAnswers = new Array<OfferedAnswer>();
  dataLoaded = false;

  constructor(private service: SurveyService, private router: Router) { }

  ngOnInit() {
    this.service.getAllOfferedAnswers().subscribe(
      res => {
        this.offeredAnswers = res as Array<OfferedAnswer>;
        this.dataLoaded = true;
      },
      err => {
        console.log(err);
      }
    );
  }

  newOA()
  {
    this.router.navigateByUrl('/offeredAnswer/add');
  }
}
