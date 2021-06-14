import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddQuestionComponent } from './components/add-question/add-question.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { SurveyComponent } from './components/survey/survey.component';

const routes: Routes = [
  {
    component: SurveyComponent,
    path: ':id'
  },
  {
    path: 'survey/add',
    component: AddSurveyComponent,
  },
  {
    path: 'question/add',
    component: AddQuestionComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
