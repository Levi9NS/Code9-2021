import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import {QuestionWithAnswersOnly, Survey } from 'src/app/models/survey';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { DateValidator } from './date-validator';


@Component({
  selector: 'app-add-survey',
  templateUrl: './add-survey.component.html',
  styleUrls: ['./add-survey.component.css']
})
export class AddSurveyComponent implements OnInit {

  surveyFormGroup: FormGroup;
  survey = new Survey();
  questionsHelper = new QuestionWithAnswersOnly();
  answersHelper: string[] = [];
  public submited = false;

  constructor(private service: SurveyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.surveyFormGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      question: ['', Validators.required],
      answers : this.formBuilder.array([])     
    },{validator: DateValidator})
  }

  get f() { return this.surveyFormGroup.controls; }

  get endDate() {
    return this.surveyFormGroup.get('endDate');
  }
  get startDate() {
    return this.surveyFormGroup.get('startDate');
  }

  get answers(){
    return this.surveyFormGroup.get('answers') as FormArray;
  }

  addAnswer(){
    this.answers.push(this.formBuilder.control(''));
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }

  onSubmit(){ 
    
    this.survey.Name = this.surveyFormGroup.controls.name.value;
    this.survey.StartDate = this.surveyFormGroup.controls.startDate.value;
    this.survey.EndDate = this.surveyFormGroup.controls.endDate.value;

    this.answersHelper = this.surveyFormGroup.controls.answers.value as Array<string>;
    this.questionsHelper.QuestionText = this.surveyFormGroup.controls.question.value;
    this.questionsHelper.Answers = this.answersHelper;

    this.survey.Questions.push(this.questionsHelper);
    console.log(this.survey);
    this.submited = true;
    this.service.addSurvey(this.survey).subscribe(
      result => {
        console.log(result);
      },
      error =>{
        console.error('error', error);
      }
    )
    
    this.router.navigateByUrl('');

  }
}
