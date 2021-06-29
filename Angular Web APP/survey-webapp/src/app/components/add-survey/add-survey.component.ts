import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import {Questions, Survey } from 'src/app/models/survey';
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
  questionsHelper: Questions[] = [];
  public submited = false;

  constructor(private service: SurveyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.surveyFormGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      questions: this.formBuilder.array([],[Validators.required])
    },{validator: DateValidator})
  }

  get f() { return this.surveyFormGroup.controls; }

  get endDate() {
    return this.surveyFormGroup.get('endDate');
  }
  get startDate() {
    return this.surveyFormGroup.get('startDate');
  }

  get questions(){
    return this.surveyFormGroup.get('questions') as FormArray;
  }

  addQuestion(){
    this.questions.push(this.formBuilder.control(''));
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }

  onSubmit(){ 
    
    this.survey.Name = this.surveyFormGroup.controls.name.value;
    this.survey.StartDate = this.surveyFormGroup.controls.startDate.value;;
    this.survey.EndDate = this.surveyFormGroup.controls.endDate.value;;
    console.log(this.survey);
    let q = this.surveyFormGroup.controls.questions.value as Array<string>;
    q.forEach(element => {
        this.questionsHelper.push({
          QuestionText : element
        });
    });
    this.survey.Questions = this.questionsHelper;
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
  }
}
