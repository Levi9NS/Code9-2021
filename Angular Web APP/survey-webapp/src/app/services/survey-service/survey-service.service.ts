import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { SurveyResult } from '../../models/survey-result';
import { SubmitSurvey } from '../../models/submit-survey';


@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getAllSurveys(): Observable<SurveyResponse[]> {
    return this.httpClient.get<SurveyResponse[]>(`${environment.apiUrl}/api/Survey/all`);
  }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}/questions`);
  }

  getSurveyAnswers(surveyId): Observable<OfferedAnswers> {
    return this.httpClient.get<OfferedAnswers>(`${environment.apiUrl}/api/Survey/${surveyId}/offered-answers`);
  }

  getSurveyResults(surveyId): Observable<SurveyResult> {
    return this.httpClient.get<SurveyResult>(`${environment.apiUrl}/api/Survey/${surveyId}/result`);
  }

  createSurvey(survey): Observable<SurveyResponse> {
    return this.httpClient.post<SurveyResponse>(`${environment.apiUrl}/api/Survey/create`,survey);
  }

  submitSurvey(submit): Observable<SubmitSurvey> {
    return this.httpClient.post<SubmitSurvey>(`${environment.apiUrl}/api/Survey/submit`,submit);
  }

  deleteSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.delete<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }
}
