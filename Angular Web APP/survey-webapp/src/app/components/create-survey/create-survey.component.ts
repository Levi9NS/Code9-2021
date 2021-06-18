import { Component, OnInit } from '@angular/core';
import { CreateSurvey } from '../../models/create-survey';
import { FormGroup, FormArray, FormBuilder, Validators} from '@angular/forms'
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { Router } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'app-create-survey',
  templateUrl: './create-survey.component.html',
  styleUrls: ['./create-survey.component.css']
})
export class CreateSurveyComponent implements OnInit {
  public survey : FormGroup;
  public success=false;
  public error=false;
  public errorText:string;
  public minDatStart:Date;
  public dateError=false;
  public submitted=false;

  constructor(private readonly surveyService: SurveyService,private _fb: FormBuilder,private router: Router) { 
    const currentDate = Date.now()
    this.minDatStart = new Date(currentDate);
  }

  ngOnInit() {
    this.survey= this._fb.group({
        name : ['',[
          Validators.required
        ]],
        startDate :  ['',[
          Validators.required
        ]],
        endDate :  ['',[
          Validators.required
        ]],
        questions: this._fb.array([this.initQuestion()])
    });
  }

  initQuestion() {
    return this._fb.group({
      questionText: ['',[
        Validators.required,
        Validators.pattern(".*[a-zA-Z0-9= ][?]$")
      ]],
      offeredAnswers: this._fb.array([this.initOfferedAnswer(),this.initOfferedAnswer()])
    });
  }
  initOfferedAnswer() {
    return this._fb.group({
      text: ['',[
        Validators.required,
       RxwebValidators.unique()
      ]]
    });
  }

  getStartDate(){
    return this.survey.get('startDate').value;
  }

  getEndDate(){
    return this.survey.get('endDate').value;
  }

  getQuetionText(i){
    return this.survey.get('questions')['controls'][i].get('questionText')
  }

  getOfferedAnswer(i,j){
    return this.survey.get('questions')['controls'][i].get('offeredAnswers')['controls'][j].get('text');
  }

  getQuestions(form) {
    return form.controls.questions.controls;
  }
  getOfferedAnsvers(form) {
    return form.controls.offeredAnswers.controls;
  }

  addQuestion(){
    const control = <FormArray>this.survey.get('questions');
    control.push(this.initQuestion());
  }

  addOfferedAnsver(i){
    const control = <FormArray>this.survey.get('questions')['controls'][i].get('offeredAnswers');
    control.push(this.initOfferedAnswer());
  }

  removeQuestion(i){
    const control = <FormArray>this.survey.get('questions');
    control.removeAt(i);
  }

  removeOfferedAnsver(i,j){
    const control = <FormArray>this.survey.get('questions')['controls'][i].get('offeredAnswers');
    control.removeAt(j);
  }

  
   
  submitSurvey(){
    this.submitted=true;
    this.surveyService.createSurvey(this.survey.value).subscribe(
      answer=> {
      this.success=true;
      setTimeout(() => {
        this.router.navigate(['']);
      }, 2000);
    },error=>{
      this.error=true;
      this.errorText=error
    });
  }
  
  showRemovieQuestion(){
    const control = <FormArray>this.survey.get('questions');
    if(control.length>1){
      return 'visible';
    }
    return 'hidden';
  }

  showRemovieAnswer(i){
    const control = <FormArray>this.survey.get('questions')['controls'][i].get('offeredAnswers');
    if(control.length>2){
      return 'visible';
    }
    return 'hidden';
  }
}
