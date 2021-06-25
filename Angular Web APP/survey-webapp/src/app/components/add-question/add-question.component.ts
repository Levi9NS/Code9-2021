import { Component, OnInit } from '@angular/core';
import { stringify } from '@angular/core/src/util';
import { ActivatedRoute, Router } from '@angular/router';
import { OfferedAnswer } from 'src/app/models/offered-answer';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {

  surveyId;
  public id: number = 0;
  public questionText: string = "";
  offeredAnswers = new Array<OfferedAnswer>();
  answers=[];
  dataLoaded = false;

  constructor(private service: SurveyService, private route: ActivatedRoute, private router: Router) {
    route.params.subscribe(params => {this.surveyId = params['id'];});
   }

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

  onChange(answerId: number, event)
  {
    const checked = event.target.checked;

    if (checked)
    {
      this.answers.push(answerId);
    }
    else
    {
      const index = this.answers.indexOf(answerId);
      this.answers.splice(index, 1);
    }
  }

  onSubmit() {
    if (this.questionText != ""){
      if (this.surveyId == undefined){
        this.service.addQuestion(this.id, this.questionText, this.answers).subscribe(
          (res: any) => {
            this.router.navigateByUrl('survey/getAllQuestions');
          },
          err => {
            console.log(err);
          }
        );
      }
      else
      {
        this.service.addQuestionToSurvey(this.surveyId, this.id, this.questionText, this.answers).subscribe(
          (res: any) => {
            this.router.navigateByUrl('/' + this.surveyId + '/questions');
          },
          err => {
            console.log(err);
          }
        );
      }
    }
    else{
      alert("Text field cannot be empty");
    }
  }
}
