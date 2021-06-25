import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { Survey } from 'src/app/models/survey';
import { Participant } from 'src/app/models/Participant';
import { SurveyResult } from 'src/app/models/SurveyResult';
import { Result } from 'src/app/models/Results';

@Injectable({
  providedIn: 'root'
})


export class SurveyService {
  api_url=environment.apiUrl;

  constructor(private readonly httpClient: HttpClient) { }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }

  getSurveyAnswers(surveyId): Observable<OfferedAnswers> {
    return this.httpClient.get<OfferedAnswers>(`${environment.apiUrl}/api/Survey/${surveyId}/OfferedAnswers`);
  }

  

  AddSurvey(survey: Survey) 
  {
    console.log(survey);
    var nesto=this.httpClient.post<Survey>(this.api_url+ '/api/Survey',survey); 
    console.log(JSON.stringify(survey));
    return nesto;
  }

  AddParticipant(participant: Participant)
  {
    return this.httpClient.post<Participant>(this.api_url+ '/api/Survey/AddParticipant',participant); 
  }
  
  AddResult(surveyResult: SurveyResult)
  {
    return this.httpClient.post<SurveyResult>(this.api_url+ '/api/Survey/AddResult',surveyResult); 
  }

  GetSurveys()
  {
    return this.httpClient.get<any>(`${environment.apiUrl}/api/Survey/GetSurveys`);
  }

  getSurveyResults(surveyId): Observable<Result[]> {
    return this.httpClient.get<Result[]>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }
}
