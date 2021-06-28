import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgModel, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Participant } from 'src/app/models/participant-model';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { Model } from 'survey-angular';

@Component({
  selector: 'app-participant-data',
  templateUrl: './participant-data.component.html',
  styleUrls: ['./participant-data.component.css']
})
export class ParticipantDataComponent implements OnInit {
  surveyId = 0;
  participant = new Participant();
  dataloaded = false;

  constructor(private service: SurveyService, private router: Router) { 
  }

  ngOnInit() {
    this.surveyId = history.state.surveyId;
    this.dataloaded = true;
  }

  onSubmit(data)
  {
    this.participant = data as Participant;
    this.participant.surveyId = this.surveyId;
    this.service.addParticipant(this.participant).subscribe(
      //result => console.log(this.participant),
      error => console.error('error', error)
    );
    console.log(this.participant);
    this.router.navigateByUrl('/'+this.surveyId, {state:{id: this.surveyId}});
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }
}
