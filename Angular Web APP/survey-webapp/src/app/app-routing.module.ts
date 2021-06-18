import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './components/survey/survey.component';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { HomeComponent } from './components/home/home.component';
import { ResultComponent } from './components/result/result.component';

const routes: Routes = [
  {
    component: HomeComponent,
    path: ''
  },
  {
    component: SurveyComponent,
    path: 'survey/:id'
  },
  {
    component: CreateSurveyComponent,
    path: 'create'
  },
  {
    component: ResultComponent,
    path: 'result/:id'
  },
  {path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
