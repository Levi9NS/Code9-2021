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
  questions: Question[];
  surveys: SurveyResponse[];
  allQuestions: any;
  allSurveys: any;

  constructor(private surveyService: SurveyService) { }

  async ngOnInit() {
    this.initForms();
    this.allQuestions = await this.surveyService.getAllQuestions();
    this.allSurveys = await this.surveyService.getAllSurveys();
  }

  initForms() {
    this.addSurveyForm = new FormGroup({
      description: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required)
    });
    this.addQuestionForm = new FormGroup({
      survey: new FormControl(this.surveys, Validators.required),
      question: new FormControl(this.questions, Validators.required)
    });
  }

  async onSubmit(form: FormGroup) {
    switch (form) {
      case this.addSurveyForm:
        const description = this.addSurveyForm.controls['description'].value
        const endDate = this.addSurveyForm.controls['endDate'].value
        await this.surveyService.addSurvey(description, endDate);
        this.allSurveys = await this.surveyService.getAllSurveys()
        break;
      case this.addQuestionForm:
        const surveyId = this.addQuestionForm.controls['survey'].value
        const questionId = this.addQuestionForm.controls['question'].value
        await this.surveyService.addQuestionToSurvey(surveyId, questionId)
        break;
    }
    form.reset();
  }
}