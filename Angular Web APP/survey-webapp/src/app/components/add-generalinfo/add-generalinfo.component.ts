import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GeneralInformations } from 'src/app/models/general-informations';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';

@Component({
  selector: 'app-add-generalinfo',
  templateUrl: './add-generalinfo.component.html',
  styleUrls: ['./add-generalinfo.component.css']
})
export class AddGeneralinfoComponent implements OnInit {
  generalInformations : GeneralInformations;
  constructor(private service: SurveyService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(data){
    this.generalInformations = data as GeneralInformations;
    console.log(this.generalInformations);
    this.service.addGeneralInformations(this.generalInformations).subscribe(
      result => {
        console.log(result);
      },
      error => {
        console.error('error', error);
      }
    );
    
    this.router.navigateByUrl('');
  }

  OnCancel(){
    this.router.navigateByUrl('');
  }
}
