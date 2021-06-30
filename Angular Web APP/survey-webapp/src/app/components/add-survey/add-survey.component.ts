import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-add-survey',
  templateUrl: './add-survey.component.html',
  styleUrls: ['./add-survey.component.css']
})
export class AddSurveyComponent implements OnInit {
  addSurveyForm: FormGroup;
  addQuestionForm: FormGroup;
  allQuestions: any;
  allSurveys: any;
  showAddSurveyError = false;
  showAddQuestionToSurveyError = false;

  constructor(private surveyService: SurveyService, private spinner: NgxSpinnerService) { }

  async ngOnInit() {
    this.spinner.show();
    this.initForms();
    this.allQuestions = await this.surveyService.getAllQuestions();
    this.allSurveys = await this.surveyService.getAllSurveys();
    this.spinner.hide();
  }

  initForms() {
    this.addSurveyForm = new FormGroup({
      description: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required)
    });
    this.addQuestionForm = new FormGroup({
      survey: new FormControl(this.allSurveys, Validators.required),
      question: new FormControl(this.allQuestions, Validators.required)
    });
  }

  async onSubmit(form: FormGroup) {
    switch (form) {
      case this.addSurveyForm:
        if (this.addSurveyForm.valid) {
          this.showAddSurveyError = false;
          this.spinner.show();
          const description = this.addSurveyForm.controls['description'].value;
          const endDate = this.addSurveyForm.controls['endDate'].value;
          await this.surveyService.addSurvey(description, endDate);
          this.allSurveys = await this.surveyService.getAllSurveys();
          form.reset();
          this.spinner.hide();
        } else {
          this.showAddSurveyError = true;
        }
        break;
      case this.addQuestionForm:
        if (this.addQuestionForm.valid) {
          this.showAddQuestionToSurveyError = false;
          this.spinner.show();
          const surveyId = this.addQuestionForm.controls['survey'].value;
          const questionId = this.addQuestionForm.controls['question'].value;
          await this.surveyService.addQuestionToSurvey(surveyId, questionId);
          form.reset();
          this.spinner.hide();
        } else {
          this.showAddQuestionToSurveyError = true;
        }
        break;
    }
  }
}