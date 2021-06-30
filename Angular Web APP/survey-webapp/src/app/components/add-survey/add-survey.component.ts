import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { SurveyService } from '../../services/survey-service/survey-service.service';

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
  dataLoaded = false;
  errorFirstForm = false;
  errorSecondForm = false;

  constructor(private surveyService: SurveyService) { }

  async ngOnInit() {
    this.initForms();
    this.allQuestions = await this.surveyService.getAllQuestions();
    this.allSurveys = await this.surveyService.getAllSurveys();
    this.dataLoaded = true;
  }

  initForms() {
    this.addSurveyForm = new FormGroup({
      description: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required)
    });
    this.addQuestionForm = new FormGroup({
      survey: new FormControl(this.allQuestions, Validators.required),
      question: new FormControl(this.allSurveys, Validators.required)
    });
  }

  async onSubmit(form: FormGroup) {
    switch (form) {
      case this.addSurveyForm:
        if (this.addSurveyForm.valid) {
          this.errorFirstForm = false;
          this.dataLoaded = false;
          const description = this.addSurveyForm.controls['description'].value
          const endDate = this.addSurveyForm.controls['endDate'].value
          await this.surveyService.addSurvey(description, endDate);
          this.allSurveys = await this.surveyService.getAllSurveys()
          this.dataLoaded = true;
        } else {
          this.errorFirstForm = true;
        }
        break;
      case this.addQuestionForm:
        if (this.addQuestionForm.valid) {
          this.errorSecondForm = false;
          this.dataLoaded = false;
          const surveyId = this.addQuestionForm.controls['survey'].value
          const questionId = this.addQuestionForm.controls['question'].value
          await this.surveyService.addQuestionToSurvey(surveyId, questionId)
          this.dataLoaded = true;
        } else {
          this.errorSecondForm = true;
        }
        break;
    }
  }
}