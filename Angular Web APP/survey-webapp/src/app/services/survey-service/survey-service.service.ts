import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { SurveyModel } from 'src/app/models/survey-model';
import { FormBuilder, Validators } from '@angular/forms';
import { SurveyQuestion } from 'src/app/models/survey-question';
import { ShortSurveyModel } from 'src/app/models/survey-button-model';
import { SurveyResultModel } from 'src/app/models/survey-result-model';
import { OfferedAnswer } from 'src/app/models/offered-answer';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }

  getSurveyAnswers(surveyId): Observable<OfferedAnswers> {
    return this.httpClient.get<OfferedAnswers>(`${environment.apiUrl}/api/Survey/${surveyId}/OfferedAnswers`);
  }

  // for viewing and editing
  getSurveyQuestions(surveyId): Observable<SurveyQuestion[]> {
    return this.httpClient.get<SurveyQuestion[]>(`${environment.apiUrl}/api/Survey/${surveyId}/questions`);
  }

  getSurveyDetails(surveyId): Observable<ShortSurveyModel>
  {
    return this.httpClient.get<ShortSurveyModel>(`${environment.apiUrl}/api/Survey/${surveyId}/short`);
  }

  getDetailsOfSurveys(): Observable<ShortSurveyModel[]>
  {
    return this.httpClient.get<ShortSurveyModel[]>(`${environment.apiUrl}/api/Survey/survey/shorts`);
  }

  getSurveyResults(surveyId): Observable<SurveyResultModel>
  {
    return this.httpClient.get<SurveyResultModel>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }

  // adding general question
  addQuestion(id: number, text: string, answers)
  {
    var body = {
      Id: id,
      QuestionText: text,
      Answers: answers
    };
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/question/add`, body);
  }

  addQuestionToSurvey(surveyId, id: number, text: string, answers)
  {
    var body = {
      Id: id,
      QuestionText: text,
      SurveyId: parseInt(surveyId),
      Answers: answers
    };
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/question/add/survey`, body);
  }

  addOfferedAnswer(id: number, text: string)
  {
    var body = {
      Id: id,
      Text: text
    };

    return this.httpClient.post(`${environment.apiUrl}/api/Survey/offeredAnswer/add`, body);
  }

  deleteQuestion(id)
  {
    return this.httpClient.delete(`${environment.apiUrl}/api/Survey/survey/getAllQuestions/` + id);
  }

  removeQuestion(surveyId, id)
  {
    return this.httpClient.delete(`${environment.apiUrl}/api/Survey/${surveyId}/questions/` + id);
  }

  linkQuestion(surveyId, questionId)
  {
    var body = {
      SurveyId: parseInt(surveyId),
      QuestionId: parseInt(questionId),
    };
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/survey/linkQuestion/`, body);
  }

  getAllQuestions(): Observable<SurveyQuestion[]>
  {
    return this.httpClient.get<SurveyQuestion[]>(`${environment.apiUrl}/api/Survey/survey/getAllQuestions`);
  }

  getAllOfferedAnswers(): Observable<OfferedAnswer[]>
  {
    return this.httpClient.get<OfferedAnswer[]>(`${environment.apiUrl}/api/Survey/survey/getAllOfferedAnswers`);
  }

  addSurvey(body: any){
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/survey/add`, body);
  }

  sendAnswer(body: any){
    body.Id = 1;
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/survey/submit`, body);
  }
}
