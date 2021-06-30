import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {

  addQuestionForm: FormGroup;
  addOfferedAnswerForm: FormGroup;
  allOfferedAnswers: any;
  dataLoaded = false;
  errorFirstForm = false;
  errorSecondForm = false;

  constructor(private surveyService: SurveyService) { }

  async ngOnInit() {
    this.initForm()
    this.allOfferedAnswers = await this.surveyService.getOfferedAnswers();
    this.dataLoaded = true;
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
          this.errorFirstForm = false;
          this.dataLoaded = false;
          const questionText = this.addQuestionForm.controls['questionText'].value;
          const offeredAnswersIds = this.addQuestionForm.controls['offeredAnswers'].value;
          await this.surveyService.addQuestion(questionText, offeredAnswersIds);
          this.dataLoaded = true;
        } else {
          this.errorFirstForm = true;
        }
        break;
      case this.addOfferedAnswerForm:
        if (this.addOfferedAnswerForm.valid) {
          this.errorSecondForm = false;
          this.dataLoaded = false;
          const offeredAnswerText = this.addOfferedAnswerForm.controls['offeredAnswerText'].value;
          await this.surveyService.addOfferedAnswer(offeredAnswerText);
          this.allOfferedAnswers = await this.surveyService.getOfferedAnswers();
          this.dataLoaded = true;
        } else {
          this.errorSecondForm = true;
        }
        break;
    }
  }
}
