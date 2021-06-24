import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { SurveyResponse } from 'src/app/models/survey-response';
import { Question } from 'survey-angular';
import { SurveyService } from '../../services/survey-service/survey-service.service';

@Component({
  selector: 'app-add-survey',
  templateUrl: './add-survey.component.html',
  styleUrls: ['./add-survey.component.css']
})
export class AddSurveyComponent implements OnInit {
  addSurveyForm: FormGroup;
  addQuestionForm: FormGroup;
  addQuestionAnswerForm: FormGroup;
  questions: Question[];
  surveys: SurveyResponse[];

  constructor(private readonly surveyService: SurveyService) { }

  allQuestions = this.surveyService.getAllQuestions().subscribe(q => { this.questions = q as Question[]; }, err => { console.log(err); });
  allSurveys = this.surveyService.getAllSurveys().subscribe(s => { this.surveys = s as SurveyResponse[] }, err => { console.log(err); });

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm() {
    this.addSurveyForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(4)]),
      date: new FormControl('', Validators.required)
    });
    this.addQuestionForm = new FormGroup({
      survey: new FormControl(this.surveys, Validators.required),
      text: new FormControl('', [Validators.required, Validators.minLength(8)])
    });
    this.addQuestionAnswerForm = new FormGroup({
      question: new FormControl(this.questions, Validators.required),
      text: new FormControl('', [Validators.required, Validators.minLength(2)])
    });
  }

  onSubmit(form: FormGroup) {
    if (form == this.addQuestionAnswerForm) {
      this.surveyService.addOfferedAnswerToQuestion(this.addQuestionAnswerForm.controls['question'].value, this.addQuestionAnswerForm.controls['text'].value)
        .subscribe(err => { console.log(err); });
    }
    else if (form == this.addQuestionForm) {
      this.surveyService.addQuestionToSurvey(this.addQuestionForm.controls['survey'].value, this.addQuestionForm.controls['text'].value)
        .subscribe(err => { console.log(err); }); 
    }
    else if (form == this.addSurveyForm) {
      this.surveyService.addSurvey(this.addSurveyForm.controls['name'].value, this.addSurveyForm.controls['date'].value)
        .subscribe(err => { console.log(err); });
    }
    form.reset();
  }
}
