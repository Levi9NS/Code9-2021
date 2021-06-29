import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { OfferedAnswersModel } from 'src/app/models/answers-response';
import { QuestionAndAnswers } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';


@Component({
  selector: 'app-add-questions',
  templateUrl: './add-questions.component.html',
  styleUrls: ['./add-questions.component.css']
})
export class AddQuestionsComponent implements OnInit {
  dataloaded = false;
  answers = Array<OfferedAnswersModel>();
  offeredAnswers: FormGroup;
  questionsFormGroup : FormGroup;
  question: QuestionAndAnswers;
  surveyId: number;

  constructor(private service: SurveyService, private router: Router, private fromBuilder: FormBuilder) { }

  ngOnInit() {
    this.surveyId = history.state.surveyId;
    this.questionsFormGroup= this.fromBuilder.group(
      {
        question: [''],
        answers : this.fromBuilder.group({
          answer: ['']
        }),
      }
    );

    this.service.getAllOfferedAnswers().subscribe(
      result => {
        this.answers = result;
        this.dataloaded = true;
        console.log(result);
      }
    );

    
    console.log();
  }

  onSubmit(){
    this.question = this.questionsFormGroup.value as QuestionAndAnswers;
    this.question.surveyId = this.surveyId;
    this.offeredAnswers = this.questionsFormGroup.get('answers') as FormGroup;
    let pickedAnswers = this.offeredAnswers.value as OfferedAnswersModel;
    console.log(pickedAnswers);
    console.log(this.question);
    this.service.addQuestion(this.question).subscribe(
      result => {
        console.log(this.question);
      },
      error=>{
        console.error('error', error);
      }
    )
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }
}
