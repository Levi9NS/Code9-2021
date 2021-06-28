import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GeneralInformations } from 'src/app/models/general-informations';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers:[DatePipe]
})
export class HomeComponent implements OnInit {
  generalInformations = Array<GeneralInformations>();
  dataLoaded = false;
  
  constructor(private service: SurveyService, private router: Router, private datepipe: DatePipe) { }

  ngOnInit() {
    this.service.getAllSurveys().subscribe(
      result => {
        this.generalInformations = result as Array<GeneralInformations>;
        this.dataLoaded = true;
        this.generalInformations.forEach( iteam => {
          
          let date = new Date();
          let current_date = this.datepipe.transform(date, 'yyyy-MM-dd');
          let end_date = this.datepipe.transform(iteam.endDate, 'yyyy-MM-dd');
          let start_date = this.datepipe.transform(iteam.startDate, 'yyyy-MM-dd');

          if(current_date > end_date || current_date < start_date){
            iteam.isOpen = false;
          }
          else{
            iteam.isOpen = true;
          }
         
          
        })
      }
    );
  }

  doSurvey(id:number){
    this.router.navigateByUrl('/participant/Add', {state:{ surveyId: id}});
  }
  
  addQuestions(id:number){
    this.router.navigateByUrl('/questionAndAnswers/Add', {state:{ surveyId: id}});
  }
  
  
  getSurveyResults(id:number){
    this.router.navigateByUrl('/'+id+'/Answers', {state:{surveyId: id}})
  }

  addNewSurvey(){
    this.router.navigateByUrl('/newSurvey/Add');
  }
}
