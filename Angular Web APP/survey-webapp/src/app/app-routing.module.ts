import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './components/survey/survey.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { InitialPageComponent } from './components/initial-page/initial-page.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { AddQuestionComponent } from './components/add-question/add-question.component';

const routes: Routes = [
  {
    component: InitialPageComponent,
    path: ''
  },
  {
    component: SurveyComponent,
    path: ':id'
  },
  {
    component: AddSurveyComponent,
    path: 'add/survey'
  },
  {
    component: SurveyResultsComponent,
    path: 'results/:id',
  },
  {
    component: AddQuestionComponent,
    path: 'add/question',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
