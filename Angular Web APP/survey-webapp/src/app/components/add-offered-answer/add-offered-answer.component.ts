import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-add-offered-answer',
  templateUrl: './add-offered-answer.component.html',
  styleUrls: ['./add-offered-answer.component.css']
})
export class AddOfferedAnswerComponent implements OnInit {

  public id: number = 0;
  public text: string = "";

  constructor(private service: SurveyService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
  }

  onSubmit()
  {
    if (this.text != ""){
      this.service.addOfferedAnswer(this.id, this.text).subscribe(
        (res: any) => {
          this.router.navigateByUrl('/survey/getAllOfferedAnswers');
        },
        err => {
          console.log(err);
        }
      );
    }
    else{
      alert("Text field cannot be empty");
    }
  }
}
