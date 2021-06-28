import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './components/survey/survey.component';
import { HomeComponent } from './components/home/home.component';
import { ParticipantDataComponent } from './components/participant-data/participant-data.component';
import { ResultComponent } from './components/result/result.component';
import { AddQuestionsComponent } from './components/add-questions/add-questions.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';


const routes: Routes = [
  {
    component: SurveyComponent,
    path: ':id'
  },
  {
    component: ParticipantDataComponent,
    path: 'participant/Add',
    pathMatch: 'full'
  },
  {
    component: ResultComponent,
    path: ':id/Answers'
  },
  {
    component: AddQuestionsComponent,
    path: 'questionAndAnswers/Add'
  },
  {
    component: AddSurveyComponent,
    path: 'newSurvey/Add'
  },
  {
    component: HomeComponent,
    path: '**',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
