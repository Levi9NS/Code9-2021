import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyQuestion } from 'src/app/models/survey-question';
import { SurveyQuestionAdditional } from 'src/app/models/survey-question-additional';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  surveyId;
  questions = new Array<SurveyQuestion>();
  surveyQuestions = new Array<SurveyQuestion>();
  existingQuestions = new Map<number, SurveyQuestionAdditional>();
  dataLoaded = false;

  constructor(private service: SurveyService, private route: ActivatedRoute, private router: Router) { 
    route.params.subscribe(params => {this.surveyId = params['id'];});
  }

  ngOnInit() {
    this.service.getAllQuestions().subscribe(
      res => {
        this.questions = res as Array<SurveyQuestion>;
        this.dataLoaded = true;
        this.questions.forEach(element => {
          var f = new SurveyQuestionAdditional();
          f.id = element.id;
          f.questionText = element.questionText;
          f.createdBy = element.createdBy;
          f.createDate = element.createDate;
          f.linked = false;
          this.existingQuestions.set(f.id, f);
        });
      },
      err => {
        console.log(err);
      }
    );

    if (this.surveyId != undefined)
    {
      this.service.getSurveyQuestions(this.surveyId).subscribe(
        res => {
          this.surveyQuestions = res as Array<SurveyQuestion>;
          this.surveyQuestions.forEach(element =>{
            var f = this.existingQuestions.get(element.id);
            f.linked = true;
          });
        }, err =>
        {
          console.log(err);
        }
      );
    }
  }

  newQuestion()
  {
    this.router.navigateByUrl('/question/add');
  }

  deleteQuestion(id: number)
  {
    this.service.deleteQuestion(id).subscribe(
      res => {
        alert("Question with id: " + id + " successfully deleted!");
        location.reload();
      },
      err => {
        console.log(err);
      }
    );
  }

  linkQuestion(questionId: number)
  {
    this.service.linkQuestion(this.surveyId, questionId).subscribe(
      res =>
      {
        location.reload();
      }, err =>
      {
        console.log(err);
      }
    );
  }
}
