import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getSurvey(surveyName: string): Observable<any> {
    return this.httpClient.get(`${environment.apiUrl}/Survey/Details/?surveyName=${surveyName}`);
  }
}
