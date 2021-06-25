import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SurveyResponse } from '../models/survey-response';
import { SurveyService } from '../services/survey-service/survey-service.service';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.css']
})
export class StartPageComponent implements OnInit {

  surveys: SurveyResponse[]; 
  constructor(private router: Router,private surveyService: SurveyService) { }

  ngOnInit() {
  this.surveyService.getSurveyResults(1).subscribe(
    (result)  => {
      console.log(result);
      
    }
  );
   
      this.surveyService.GetSurveys().subscribe(
        (result)  => {
          console.log(result);
          this.surveys=result;
        }
      );

  }

  onOpen(id: number)
  {
    this.router.navigate(['/'+id]);
  }

  createNew() {
    this.router.navigate(['/1/Create']);
  }

  onResults(id: number)
  {
    this.router.navigate(['/'+id+ '/Results']);
  }
}
