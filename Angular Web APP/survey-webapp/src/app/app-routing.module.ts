import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddOfferedAnswerComponent } from './components/add-offered-answer/add-offered-answer.component';
import { AddQuestionComponent } from './components/add-question/add-question.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { HomeComponent } from './components/home/home.component';
import { OfferedAnswersComponent } from './components/offered-answers/offered-answers.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { SurveyQuestionsComponent } from './components/survey-questions/survey-questions.component';
import { SurveyResultComponent } from './components/survey-result/survey-result.component';
import { SurveyComponent } from './components/survey/survey.component';
import { TestComponentComponent } from './components/test-component/test-component.component';

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
  },
  {
    path: 'question/add/:id',
    component: AddQuestionComponent
  },
  {
    path: ':id/questions',
    component: SurveyQuestionsComponent
  },
  {
    path: 'survey/getAllQuestions',
    component: QuestionsComponent
  },
  {
    path: 'survey/getAllQuestions/:id',
    component: QuestionsComponent
  },
  {
    path: 'survey/home',
    component: HomeComponent
  },
  {
    path: '',
    redirectTo: '/survey/home',
    pathMatch: 'full'
  },
  {
    path: ':id/result',
    component: SurveyResultComponent
  },
  {
    path: 'offeredAnswer/add',
    component: AddOfferedAnswerComponent
  },
  {
    path: 'survey/getAllOfferedAnswers',
    component: OfferedAnswersComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
