import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import {QuestionWithAnswersOnly, Survey } from 'src/app/models/survey';
import { QuestionAndAnswers } from 'src/app/models/survey-response';
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
  questionsHelper : QuestionWithAnswersOnly[] = [];
  answersHelper: string[] = [];
  public submited = false;

  constructor(private service: SurveyService, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.surveyFormGroup = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      questions: this.formBuilder.array([
        this.AnswersGroup()
      ])     
    },{validator: DateValidator})
  }

  private AnswersGroup(): FormGroup {
    return this.formBuilder.group({
      question: [''],
      answers: this.formBuilder.array([])
    });
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

  GetAnswersGroup(i){
    return this.questions.controls[i] as FormGroup;
  }

  addAnswers(i){
    (<FormArray>this.GetAnswersGroup(i).controls.answers).push(this.formBuilder.control(''));
  }

  addQuestions(){
    this.questions.push(this.AnswersGroup());
  }

  removeQuestion(j){
    this.questions.removeAt(j);
  }

  removeAnswer(i,j){
    (<FormArray>this.GetAnswersGroup(j).controls.answers).removeAt(i);
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }

  onSubmit(){ 
    
    this.survey.Name = this.surveyFormGroup.controls.name.value;
    this.survey.StartDate = this.surveyFormGroup.controls.startDate.value;
    this.survey.EndDate = this.surveyFormGroup.controls.endDate.value;

    let questionsArray = this.surveyFormGroup.controls['questions'].value;

    for(let i = 0; i < questionsArray.length; i++){
      let questionText = questionsArray[i].question as string;
      let answersArray = questionsArray[i].answers;
      for(let j =0; j<answersArray.length; j++){
        this.answersHelper.push(answersArray[j]);
      }
      this.questionsHelper.push({
        QuestionText: questionText,
        Answers : this.answersHelper,
      })

      this.answersHelper = [];
    } 
    this.survey.Questions = this.questionsHelper;
    this.service.addSurvey(this.survey).subscribe(
      result => {
        console.log(this.survey);
        console.log(result);
      },
      error=>{
        console.error('error', error);
      }
    );

    this.OnCancel();
  }
}
