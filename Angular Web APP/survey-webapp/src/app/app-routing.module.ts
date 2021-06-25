import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { ParticipantComponent } from './components/participant/participant.component';
import { SurveyComponent } from './components/survey/survey.component';
import { ResultsComponent } from './results/results.component';
import { StartPageComponent } from './start-page/start-page.component';

const routes: Routes = [
  {
    component: CreateSurveyComponent,
    path: 'create',
  },
  {
    component: ResultsComponent,
    path: ':id/Results',
  },
  {
    component: SurveyComponent,
    path: ':id',
  },
  {
    path: '**',
    component: StartPageComponent,
    pathMatch: 'full',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
