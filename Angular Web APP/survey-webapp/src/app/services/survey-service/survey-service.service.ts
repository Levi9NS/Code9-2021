import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { Question } from 'survey-angular';
import { SurveyResult } from 'src/app/models/survey-result';
import { SurveyAnswer } from 'src/app/models/survey-answer';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getAllSurveys(): Observable<SurveyResponse[]> {
    return this.httpClient.get<SurveyResponse[]>(`${environment.apiUrl}/api/Survey`);
  }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }

  getSurveyAnswers(surveyId): Observable<OfferedAnswers> {
    return this.httpClient.get<OfferedAnswers>(`${environment.apiUrl}/api/Survey/${surveyId}/OfferedAnswers`);
  }

  getAllQuestions(): Observable<Question[]> {
    return this.httpClient.get<Question[]>(`${environment.apiUrl}/api/Survey/Questions`);
  }

  getSurveyResults(surveyId): Observable<SurveyResult> {
    return this.httpClient.get<SurveyResult>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }

  getOfferedAnswers(surveyId): Observable<OfferedAnswers[]> {
    return this.httpClient.get<OfferedAnswers[]>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }

  addSurvey(name: string, date: string) {
    var body = {
      Name: name,
      EndDate: date
    };
    console.log(body);
    return this.httpClient.post(`${environment.apiUrl}/api/Survey`, body);
  }

  addQuestionToSurvey(surveyId: number, text: string) {
    var body = {
      QuestionText: text,
      SurveyQuestionRelations: [{
        SurveyId: surveyId
      }]
    };
    console.log(body);
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/Questions`, body);
  }

  addOfferedAnswerToQuestion(id: number, text: string) {
    var body = {
      QuestionId: id,
      QuestionAnswer: text
    };
    console.log(body);
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/OfferedAnswers`, body);
  }

  addSurveyParticipant(surveyId: number, name: string, lastname: string, email: string, password: string) {
    var body = {
      SurveyId: surveyId,
      FirstName: name,
      LastName: lastname,
      Email: email,
      Password: password
    };
    console.log(body);
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/Participants`, body);
  }

  addSurveyResult(sid: number, pname: string, plastname: string, pemail: string, ppass: string, answers: SurveyAnswer[]) {
    var body = {
      SurveyId: sid,
      FirstName: pname,
      LastName: plastname,
      Email: pemail,
      Password: ppass,
      Answers: answers
    };
    console.log(body);
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/Answers`, body);
  }

}
