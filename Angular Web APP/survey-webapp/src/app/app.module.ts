import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './components/survey/survey.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { ParticipantComponent } from './components/participant/participant.component';
import { StartPageComponent } from './start-page/start-page.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ResultsComponent } from './results/results.component';
@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    HeaderComponent,
    CreateSurveyComponent,
    ParticipantComponent,
    StartPageComponent,
    ResultsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule, ReactiveFormsModule, BrowserAnimationsModule,
    MatDialogModule,
    NgbModule
  ],


  providers: [],

  bootstrap: [AppComponent]
})
export class AppModule { }
