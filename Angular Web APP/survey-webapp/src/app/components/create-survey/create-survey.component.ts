import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { OfferedAnswer, Question, Survey } from 'src/app/models/survey';
import { SurveyResponse } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { AddAnswersComponent } from './add-answers/add-answers.component';

@Component({
  selector: 'app-create-survey',
  templateUrl: './create-survey.component.html',
  styleUrls: ['./create-survey.component.css']
})
export class CreateSurveyComponent implements OnInit {
  question: string='';
  addquestion:Question;
  survey : Survey;
  newFormGroup: FormGroup;
  constructor(private formBuilder: FormBuilder,public dialog: MatDialog,private surveyService : SurveyService) { }

  ngOnInit() {
    this.createForm();
    this.survey.questions=[];
    
  }

  createForm(){
    this.survey=new Survey();
    this.newFormGroup=this.formBuilder.group({
      name: [this.survey.name, Validators.required],

      startDate:[this.survey.startDate, Validators.required],

      endDate: [this.survey.endDate, Validators.required],
    });
  }

  AddSurvey()
  {
    this.survey.name=this.newFormGroup.controls.name.value;
    this.survey.startDate=this.newFormGroup.controls.startDate.value;
    this.survey.endDate=this.newFormGroup.controls.endDate.value;
  }


  OnSubmit(){
    this.AddSurvey();
    console.log(this.survey);
    this.surveyService.AddSurvey(this.survey).subscribe(
      result =>
      {
     
           console.log(result);
         
      });
  }

  OnAdd() {
    if(this.question != '')
    {
      this.addquestion=new Question();
      this.addquestion.offeredAnswers=[];
      this.addquestion.questionText=this.question;
      this.survey.questions.push(this.addquestion);
      this.question='';
    }
  }

  OnAddAnswer(question : Question) {
    
  console.log(question.offeredAnswers);
   
    const dialogRef = this.dialog.open(AddAnswersComponent, {
      width: '800px',
      data: {question}
    });
    

    dialogRef.afterClosed().subscribe(result => {
      if(result != "Cancel")
      {
        result.forEach(element => {
          question.offeredAnswers.push(element);
        });
      }
      });
  }
}
