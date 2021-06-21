import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Questions, SurveyResponse } from 'src/app/models/survey-response';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { Question } from 'survey-angular';

@Component({
  selector: 'app-add-survey',
  templateUrl: './add-survey.component.html',
  styleUrls: ['./add-survey.component.css']
})
export class AddSurveyComponent implements OnInit {

  survey: SurveyResponse;
  public id: number = 0;
  public name: string = "";
  public questions: Questions[] = [];
  public startDate: Date = new Date();
  public endDate: Date = new Date();

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {

  }

  onSubmit() {
    var body = {
      Id: this.id,
      Name: this.name,
      Questions: this.questions,
      StartDate: this.startDate,
      EndDate: this.endDate
    };
    var now = new Date();
    var surveyDate = new Date(body.EndDate);
    if (now > surveyDate){
      alert("Enter valid end date!");
    }
    else{
      this.surveyService.addSurvey(body).subscribe(
        res => {
          alert("New survey '"+body.Name+"' added");
          this.router.navigateByUrl('/survey/home');
        }, err =>
        {
          console.log(err);
        }
      );
    }
  }
}
