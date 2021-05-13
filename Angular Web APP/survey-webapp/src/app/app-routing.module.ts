import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyComponent } from './components/survey/survey.component';

const routes: Routes = [
  {
    component: SurveyComponent,
    path: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
