import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './components/survey/survey.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { AddSurveyComponent } from './components/add-survey/add-survey.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InitialPageComponent } from './components/initial-page/initial-page.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { AddQuestionComponent } from './components/add-question/add-question.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    HeaderComponent,
    AddSurveyComponent,
    InitialPageComponent,
    SurveyResultsComponent,
    AddQuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
