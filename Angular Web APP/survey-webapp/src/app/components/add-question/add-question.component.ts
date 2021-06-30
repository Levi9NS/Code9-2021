import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {

  addQuestionForm: FormGroup;
  addOfferedAnswerForm: FormGroup;
  allOfferedAnswers: any;
  showAddQuestionError = false;
  showAddOfferedAnswerError = false;

  constructor(private surveyService: SurveyService, private spinner: NgxSpinnerService) { }

  async ngOnInit() {
    this.spinner.show();
    this.initForm();
    this.allOfferedAnswers = await this.surveyService.getOfferedAnswers();
    this.spinner.hide();
  }

  initForm() {
    this.addQuestionForm = new FormGroup({
      questionText: new FormControl('', Validators.required),
      offeredAnswers: new FormControl([], Validators.required)
    });
    this.addOfferedAnswerForm = new FormGroup({
      offeredAnswerText: new FormControl('', Validators.required),
    });
  }

  async onSubmit(form: FormGroup) {
    switch (form) {
      case this.addQuestionForm:
        if (this.addQuestionForm.valid) {
          this.showAddQuestionError = false;
          this.spinner.show();
          const questionText = this.addQuestionForm.controls['questionText'].value;
          const offeredAnswersIds = this.addQuestionForm.controls['offeredAnswers'].value;
          await this.surveyService.addQuestion(questionText, offeredAnswersIds);
          form.reset();
          this.spinner.hide();
        } else {
          this.showAddQuestionError = true;
        }
        break;
      case this.addOfferedAnswerForm:
        if (this.addOfferedAnswerForm.valid) {
          this.showAddOfferedAnswerError = false;
          this.spinner.show();
          const offeredAnswerText = this.addOfferedAnswerForm.controls['offeredAnswerText'].value;
          await this.surveyService.addOfferedAnswer(offeredAnswerText);
          this.allOfferedAnswers = await this.surveyService.getOfferedAnswers();
          form.reset();
          this.spinner.hide();
        } else {
          this.showAddOfferedAnswerError = true;
        }
        break;
    }
  }
}
