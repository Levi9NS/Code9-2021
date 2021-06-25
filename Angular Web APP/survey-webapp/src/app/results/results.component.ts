import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Result } from '../models/Results';
import { SurveyService } from '../services/survey-service/survey-service.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit {

  surveyid: string='';
  results: Result[]=[];
  numberofparticipants: number=0;
  constructor(private surveyService: SurveyService,private route:ActivatedRoute) { }
 
 
 
  
  ngOnInit() {
    this.surveyid=this.route.snapshot.paramMap.get('id');

    this.surveyService.getSurveyResults(+this.surveyid).subscribe(
      (result) => {
       
        this.results=result;
        console.log(this.results);
        
      });
    }
  

}
