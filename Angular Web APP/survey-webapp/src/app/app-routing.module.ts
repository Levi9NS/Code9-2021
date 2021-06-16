import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddSurveyComponent } from './components/add-survey/AddSurvey/AddSurvey.component';
import { SurveyResultComponent } from './components/survey-result/survey-result.component';
import { SurveyComponent } from './components/survey/survey.component';

const routes: Routes = [
  {path: 'survey/:id',component: SurveyComponent},
  {path: 'add-survey',component: AddSurveyComponent},
  {path: 'survey-result',component: SurveyResultComponent}
  
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
