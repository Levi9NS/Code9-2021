import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { SurveyResult } from 'src/app/models/survey-results';

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

  getSurveyResults(surveyId: number) {
    return this.httpClient.get(`${environment.apiUrl}/api/Survey/${surveyId}/SurveyAnswers`).toPromise();
  }

  getAllQuestions() {
    return this.httpClient.get(`${environment.apiUrl}/api/Survey/Questions`).toPromise();
  }

  addQuestion(questionText: string, offeredAnswersIds: any) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/AddQuestion`, { questionText, offeredAnswersIds }).toPromise();
  }

  getAllSurveys() {
    return this.httpClient.get(`${environment.apiUrl}/api/Survey/GetAllSurveys`).toPromise();
  }

  addSurvey(description: string, endDate: string) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/AddSurvey`, { description, endDate }).toPromise();
  }

  getOfferedAnswers() {
    return this.httpClient.get(`${environment.apiUrl}/api/Survey/GetOfferedAnswers`).toPromise();
  }

  addQuestionToSurvey(surveyId: number, questionId: number) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/AddQuestionToSurvey`, { surveyId, questionId }).toPromise();
  }

  addParticipantAnswers(dataRequest: any) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/AddParticipantAnswers`, dataRequest).toPromise();
  }

  closeSurvey(id: number) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/CloseSurvey`, { id }).toPromise();
  }

  addOfferedAnswer(questionAnswer: string) {
    return this.httpClient.post(`${environment.apiUrl}/api/Survey/AddOfferedAnswer`, { questionAnswer }).toPromise();
  }
}
