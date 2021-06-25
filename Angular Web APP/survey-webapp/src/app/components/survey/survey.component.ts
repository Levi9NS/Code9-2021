import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Participant } from 'src/app/models/Participant';
import { OfferedAnswer } from 'src/app/models/survey';
import { QuestionAnswer, SurveyResult } from 'src/app/models/SurveyResult';
import { Model, StylesManager, SurveyNG } from 'survey-angular';
import { OfferedAnswers } from '../../models/answers-response';
import { QuestionModel, SurveyModel } from '../../models/survey-model';
import { Questions, SurveyResponse } from '../../models/survey-response';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { ParticipantComponent } from '../participant/participant.component';
@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})

export class SurveyComponent implements OnInit, AfterViewInit {
  participant: Participant;
  offeredAnswers: OfferedAnswers;
  surveyResult: SurveyResult;
  qurstionAnswer: QuestionAnswer;
  question: Questions;
  questions: Questions[];
  participantID: any;
  private queryParamSurveyId = 'id';
  private surveyRootElementId = 'survey-element';
  survey: SurveyResponse;
  dataLoaded = false;

  constructor(public dialog: MatDialog,private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

ngAfterViewInit(){
  setTimeout(() => {
  this.openDialog();
});
}

  ngOnInit() {
    this.offeredAnswers=new OfferedAnswers();
   this.offeredAnswers.offeredAnswers=[];
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(surveyId)
    .subscribe(surveyResponse => {
      console.log(surveyResponse);
      this.survey = surveyResponse;
      this.questions=this.survey.questions;
      console.log(this.questions);
      this.surveyService.getSurveyAnswers(surveyId)
      .subscribe(answers => {
        console.log(answers);
        this.offeredAnswers.offeredAnswers=answers.offeredAnswers;
        console.log(answers.offeredAnswers);
        const surveyModel = this.convertAPIDataToSurveyModel(this.survey, answers);
        const survey = new Model(surveyModel);
        survey.onComplete.add(this.sendDataToServer);
        SurveyNG.render(this.surveyRootElementId, {model: survey});
        
        this.dataLoaded = true;
      });
    });
  }

  convertAPIDataToSurveyModel(survey: SurveyResponse, answers: OfferedAnswers): SurveyModel {
    const surveyModel = {
      title: survey.name,
      pages: [{
        name: 'page1',
        questions: []
      }]
    } as SurveyModel;

    surveyModel.pages[0].questions = survey.questions.map<QuestionModel>(question => {
      const result = answers.offeredAnswers.filter(answer => answer.questionId === question.id);
      const offeredAnswers = result.map(res => res.questionAnswer);
     
      return {
        name: question.questionText,
        title: question.questionText,
        choices: offeredAnswers.filter(res => res.toLocaleLowerCase() !== 'other').map(r => r).sort(),
        isRequired: true,
        type: 'checkbox',
        hasOther: offeredAnswers.findIndex(res => res.toLocaleLowerCase() === 'other') !== -1
      };
    });

    return surveyModel;
  }

  sendDataToServer = (surveyResult) => {
   this.surveyResult=new SurveyResult(+this.survey.id, +this.participant.id );
   this.surveyResult.Answers=[];


   var pages=surveyResult.pages;
   pages.forEach(element => {
      var elements=element.elements;
      elements.forEach(element1 => {
      
      this.question=new Questions();
      this.question=this.survey.questions.find(e=> e.questionText==element1.name);
      console.log(this.question);
      surveyResult.data[element1.name].forEach(name => {
        
          this.qurstionAnswer=new QuestionAnswer();
          this.qurstionAnswer.QuestionId=+this.question.id;
          this.qurstionAnswer.QuestionAnswerId=+this.offeredAnswers.offeredAnswers.find(e=>e.questionAnswer==name).offeredAnswerid;
          this.surveyResult.Answers.push(this.qurstionAnswer);
          console.log(this.qurstionAnswer);
        });
      
     
      });
    });
  
    console.log(this.surveyResult);
    this.surveyService.AddResult(this.surveyResult).subscribe(
      (res) => {
        console.log(res);
      }
    );
   
  }

  openDialog(): void {

    const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.data={
          width: '500px',
          data: {participant: Participant}
        }
        dialogConfig.autoFocus=true;

    const dialogRef = this.dialog.open(ParticipantComponent, dialogConfig );

    dialogRef.afterClosed().subscribe(result => {
          this.participant=new Participant();
          console.log(this.survey);
          this.participant=result;
          this.participant.SurveyId=+this.survey.id;

          this.surveyService.AddParticipant(this.participant).subscribe(
            (result) => {
              console.log(result);
              this.participant=result;
              console.log(this.participant);
            }
          );
    });
  }
}
