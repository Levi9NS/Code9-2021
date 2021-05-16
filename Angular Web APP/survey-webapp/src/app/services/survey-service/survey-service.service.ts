import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { AnswersResponse } from '../../models/answers-response';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }

  getSurveyAnswers(surveyId): Observable<AnswersResponse> {
    return this.httpClient.get<AnswersResponse>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }
}
