import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { HomeComponent } from './components/home/home.component';
import { SurveyResultComponent } from './components/survey-result/survey-result.component';
import { SurveyComponent } from './components/survey/survey.component';

const routes: Routes = [
  {
    component: SurveyComponent,
    path: 'survey/:id'
  },
  {
    component: AddSurveyComponent,
    path: 'add/survey',
  },
  {
    component: HomeComponent,
    path: '',
  },
  {
    component: SurveyResultComponent,
    path: 'results/:id',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
