import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShortSurveyModel } from 'src/app/models/survey-button-model';
import { SurveyQuestion } from 'src/app/models/survey-question';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-survey-questions',
  templateUrl: './survey-questions.component.html',
  styleUrls: ['./survey-questions.component.css']
})
export class SurveyQuestionsComponent implements OnInit {

  survey = new ShortSurveyModel();
  surveyId;
  questions = new Array<SurveyQuestion>();
  dataLoaded = false;
  dataExists = false;

  constructor(private service: SurveyService, private route: ActivatedRoute, private router: Router) {
    route.params.subscribe(params => {this.surveyId = params['id'];});
   }

  ngOnInit() {
    this.service.getSurveyQuestions(this.surveyId).subscribe(
      res => {
        this.questions = res as Array<SurveyQuestion>;
        if (this.questions.length != 0)
        {
          this.dataExists = true;
        }
        this.service.getSurveyDetails(this.surveyId).subscribe(
          res => {
            this.survey = res as ShortSurveyModel;
          },
          err =>
          {
            console.log(err);
          }
        )
        this.dataLoaded = true;
      },
      err => {
        console.log(err);
      }
    );
  }

  removeQuestion(id: any)
  {
    this.service.removeQuestion(this.surveyId, id).subscribe(
      res => {
        alert("Question with id: " + id + " successfully removed from survey");
        location.reload();
      },
      err => {
        console.log(err);
      }
    );
  }

  newQuestion()
  {
    this.router.navigateByUrl('/question/add/' + this.surveyId);
  }

  existingQuestion()
  {
    this.router.navigateByUrl('/survey/getAllQuestions/' + this.surveyId);
  }
}
