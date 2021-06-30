import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OfferedAnswersModel } from 'src/app/models/answers-response';
import { QuestionAndAnswers } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

const storageName : string = "surveyId";

@Component({
  selector: 'app-add-questions',
  templateUrl: './add-questions.component.html',
  styleUrls: ['./add-questions.component.css']
})
export class AddQuestionsComponent implements OnInit {
  dataloaded = false;
  offeredAnswers: FormGroup;
  questionsFormGroup : FormGroup;
  question = new QuestionAndAnswers();
  answersHelper : Array<string> = [];
  error: boolean = false;
  errorMessage: string;
  surveyId: number;
  public submited = false;

  constructor(private service: SurveyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.surveyId = history.state.surveyId;
    this.CheckSurveyID();

    this.questionsFormGroup= this.formBuilder.group(
      {
        question: ['', [Validators.required]],
        answers : this.formBuilder.array([],[Validators.required])
      });
  }

  private CheckSurveyID() {
    if (typeof this.surveyId == 'undefined' && this.GetLocalStorage() === null) {
      //error occured
      this.error = true;
      this.errorMessage = "An Error Occured. You are being redirected to Home page.";
      this.EmptyLocalStorage();
      setTimeout(() => {
        console.error('An Error Occured. You are being redirected to Home page.');
        this.OnCancel();
      }, 10000);
    }
    else if (typeof this.surveyId == 'undefined') {
      //refreshed page
      this.surveyId = parseInt(this.GetLocalStorage());
      console.log('refreshed', this.surveyId);
    }
    else {
      //routed from homepage
      this.AddLocalStorage(this.surveyId);
      console.log('routed', this.surveyId);
    }
  }

  onSubmit(){
    this.submited = true;
    this.question.SurveyId = this.surveyId;
    this.question.QuestionText = this.questionsFormGroup.controls['question'].value;
    this.answersHelper = this.questionsFormGroup.controls['answers'].value as Array<string>;
    this.question.Answers = this.answersHelper;
    console.log(this.question);
    
    this.service.addQuestion(this.question).subscribe(
      result => {
        console.log(this.question);
      },
      error=>{
        console.error('error', error);
      }
    )
    this.EmptyLocalStorage();
    this.router.navigateByUrl('');
  }

  OnCancel(){
    this.EmptyLocalStorage();
    this.router.navigateByUrl('');
  }

  get f() { return this.questionsFormGroup.controls; }

  get answers(){
    return this.questionsFormGroup.get('answers') as FormArray;
  }

  get questionControl(){
    return this.questionsFormGroup.get('question');
  }

  GetLocalStorage(){
    return localStorage.getItem(storageName);
  }

  EmptyLocalStorage(){
    localStorage.removeItem(storageName);
  }

  AddLocalStorage(id:number){
    localStorage.setItem(storageName, id.toString());
  }

  getValidity(i) {
    return (<FormArray>this.questionsFormGroup.get('answers')).controls[i].errors;
  }

  getTouch(i){
    return (<FormArray>this.questionsFormGroup.get('answers')).controls[i].touched;
  }

  addAnswer(){
    this.answers.push(this.formBuilder.control(''));
  }

  removeAnswer(i){
    this.answers.removeAt(i);
  }

}
