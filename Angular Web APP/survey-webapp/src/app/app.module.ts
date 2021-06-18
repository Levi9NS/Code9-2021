import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './components/survey/survey.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { FormsModule } from '@angular/forms';
import { AddQuestionComponent } from './components/add-question/add-question.component';
import { SurveyQuestionsComponent } from './components/survey-questions/survey-questions.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { TestComponentComponent } from './components/test-component/test-component.component';
import { HomeComponent } from './components/home/home.component';
import { SurveyResultComponent } from './components/survey-result/survey-result.component';
import { AddOfferedAnswerComponent } from './components/add-offered-answer/add-offered-answer.component';
import { OfferedAnswersComponent } from './components/offered-answers/offered-answers.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    HeaderComponent,
    AddSurveyComponent,
    AddQuestionComponent,
    SurveyQuestionsComponent,
    QuestionsComponent,
    TestComponentComponent,
    HomeComponent,
    SurveyResultComponent,
    AddOfferedAnswerComponent,
    OfferedAnswersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
