import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OfferedAnswer, Question } from 'src/app/models/survey';

@Component({
  selector: 'app-add-answers',
  templateUrl: './add-answers.component.html',
  styleUrls: ['./add-answers.component.css']
})
export class AddAnswersComponent implements OnInit {
  q:Question;
  answertext: string='';
  answer: OfferedAnswer;
  constructor(public dialogRef: MatDialogRef<AddAnswersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Question) {
      console.log(data);
     }

   ngOnInit() {
    this.q=new Question();
    this.q.offeredAnswers=[];
    if(this.data.offeredAnswers != null)
    {
      this.q.offeredAnswers=this.data.offeredAnswers;
    }
   }

  OnAddAnswer(){
   
    if(this.answertext != '')
    {
      this.answer=new OfferedAnswer();
    
      this.answer.text=this.answertext;
      
        this.q.offeredAnswers.push(this.answer);
        this.answertext='';
    }
  }

  onNoClick() {
    this.dialogRef.close("Cancel");
  }

  save(){
    this.dialogRef.close(this.q.offeredAnswers);
  }

}
