import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './components/survey/survey.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { ParticipantDataComponent } from './components/participant-data/participant-data.component';
import { ResultComponent } from './components/result/result.component';
import { AddQuestionsComponent } from './components/add-questions/add-questions.component';
import { AddGeneralinfoComponent } from './components/add-generalinfo/add-generalinfo.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddSurveyComponent } from './add-survey/add-survey.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    HeaderComponent,
    HomeComponent,
    ParticipantDataComponent,
    ResultComponent,
    AddQuestionsComponent,
    AddGeneralinfoComponent,
    AddSurveyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
