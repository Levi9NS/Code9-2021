import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgModel, Validators } from '@angular/forms';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { Participant } from 'src/app/models/participant-model';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

const storageName : string = "surveyId";

@Component({
  selector: 'app-participant-data',
  templateUrl: './participant-data.component.html',
  styleUrls: ['./participant-data.component.css']
})

export class ParticipantDataComponent implements OnInit {
  surveyId = 0;
  browserRefresh : boolean;
  error: boolean = false;
  errorMessage: string;
  participant = new Participant();

  constructor(private service: SurveyService, private router: Router) { 
  }

  ngOnInit() {

    this.surveyId = history.state.surveyId; 
    this.CheckSurveyID();
  }

  private CheckSurveyID() {
    if (typeof this.surveyId == 'undefined' && this.GetLocalStorage() === null) {
      //error occured
      this.error = true;
      this.errorMessage = "An Error Occured. You are being redirected to Home page.";
      this.EmptyLocalStorage();
      setTimeout(() => {
        console.error('An Error Occured. You are being redirected to Home page.');
        this.OnCancel();
      }, 10000);
    }
    else if (typeof this.surveyId == 'undefined') {
      //refreshed page
      this.surveyId = parseInt(this.GetLocalStorage());
      console.log('refreshed', this.surveyId);
    }
    else {
      //routed from homepage
      this.AddLocalStorage(this.surveyId);
      console.log('routed', this.surveyId);
    }
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
    this.EmptyLocalStorage();
    this.router.navigateByUrl('/'+this.surveyId, {state:{id: this.surveyId}});
  }

  OnCancel(){
    this.EmptyLocalStorage();
    this.router.navigateByUrl('');
  }

  GetLocalStorage(){
    return localStorage.getItem(storageName);
  }

  EmptyLocalStorage(){
    localStorage.removeItem(storageName);
  }

  AddLocalStorage(id:number){
    localStorage.setItem(storageName, id.toString());
  }

}
