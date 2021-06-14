import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyComponent } from './components/survey/survey.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { HomeComponent } from './components/home/home.component';
import { ResultComponent } from './components/result/result.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatIconModule} from '@angular/material';

import { NgxChartsModule }from '@swimlane/ngx-charts';

import {  RxReactiveFormsModule } from "@rxweb/reactive-form-validators"

@NgModule({
  declarations: [
    AppComponent,
    SurveyComponent,
    HeaderComponent,
    CreateSurveyComponent,
    HomeComponent,
    ResultComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatChipsModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatIconModule,
    NgxChartsModule,
    RxReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
