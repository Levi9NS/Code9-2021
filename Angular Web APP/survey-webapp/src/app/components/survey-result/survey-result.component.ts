import { Component, OnInit } from '@angular/core';
import { AnsweredQuestion, AnsweredQuestionList, SurveyResult } from 'src/app/models/survey-result';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { ActivatedRoute } from '@angular/router';
import { GroupByPipe } from 'ngx-pipes';
import { from } from 'rxjs';

@Component({
  selector: 'app-survey-result',
  templateUrl: './survey-result.component.html',
  styleUrls: ['./survey-result.component.css']
})

export class SurveyResultComponent implements OnInit {
  name:string;
  questions:AnsweredQuestion[];

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) {}

  ngOnInit() {
    const surveyId = this.route.snapshot.params['id'];
    this.surveyService.getSurveyResults(surveyId).subscribe(s => { this.name = s.name as string, this.questions = s.questions as AnsweredQuestion[] }, err => { console.log(err); });
  }
}