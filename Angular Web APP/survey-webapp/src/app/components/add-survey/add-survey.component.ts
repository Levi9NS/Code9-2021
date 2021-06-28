import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { Questions, Survey } from 'src/app/models/survey';
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
    this.survey = this.surveyFormGroup.value as Survey;
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
