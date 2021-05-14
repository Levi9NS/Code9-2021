import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from 'src/app/services/survey-service/survey-service.service';
import { Model, SurveyNG } from 'survey-angular';
@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})

export class SurveyComponent implements OnInit {

  surveyJSON = { title: 'Tell us, what technologies do you use?', pages: [
    { name: 'page1', questions: [
        {
          type: 'radiogroup',
          choices: [ 'Yes', 'No' ],
          isRequired: true,
          name: 'frameworkUsing',
          title: 'Do you use any front-end framework like Bootstrap?' },
        {
          type: 'checkbox',
          choices: ['Bootstrap', 'Foundation'],
          hasOther: true,
          isRequired: true,
          name: 'framework',
          title: 'What front-end framework do you use?',
          visibleIf: '{frameworkUsing} = \'Yes\''
        },
        {
          type: 'radiogroup',
          choices: ['Yes', 'No'],
          isRequired: true,
          name: 'mvvmUsing',
          title: 'Do you use any MVVM framework?'
        },
        {
          type: 'checkbox',
          choices: [ 'AngularJS', 'KnockoutJS', 'React' ],
          hasOther: true,
          isRequired: true,
          name: 'mvvm',
          title: 'What MVVM framework do you use?',
          visibleIf: '{mvvmUsing} = \'Yes\''
        },
        {
          type: 'comment',
          name: 'about',
          title: 'Please tell us about your main requirements for Survey library'
        }
     ]},
    // {
    //   name: 'page2', questions: [
    //     {
    //       type: 'radiogroup',
    //       choices: ['Yes', 'No'],
    //       isRequired: true,
    //       name: 'mvvmUsing',
    //       title: 'Do you use any MVVM framework?'
    //     },
    //     {
    //       type: 'checkbox',
    //       choices: [ 'AngularJS', 'KnockoutJS', 'React' ],
    //       hasOther: true,
    //       isRequired: true,
    //       name: 'mvvm',
    //       title: 'What MVVM framework do you use?',
    //       visibleIf: '{mvvmUsing} = \'Yes\''
    //     }
    //   ]
    // },
    // {
    //   name: 'page3', questions: [
    //     {
    //       type: 'comment',
    //       name: 'about',
    //       title: 'Please tell us about your main requirements for Survey library'
    //     }
    //   ]
    // }
   ]
  };

  private queryParamSurveyId = 'id';

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) { }

  ngOnInit() {
    const surveyId = this.route.snapshot.params[this.queryParamSurveyId];

    this.surveyService.getSurvey(surveyId)
    .subscribe(survey => {
      this.surveyService.getSurveyAnswers(surveyId)
      .subscribe(answers => {
        debugger;
      });
    });

    const survey = new Model(this.surveyJSON);
    survey.onComplete.add(this.sendDataToServer);
    SurveyNG.render('survey-element', {model: survey});
  }

  sendDataToServer = (surveyResult) => {
    console.log(surveyResult.data);
    debugger;
  }

}
