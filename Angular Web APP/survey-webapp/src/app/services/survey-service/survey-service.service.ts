import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyResponse } from 'src/app/models/survey-response';
import { OfferedAnswers } from '../../models/answers-response';
import { Answer } from 'src/app/models/answer';
import { OfferedAnswerOriginal, OfferedAnswersOriginals } from 'src/app/models/offered-answer-original';
import { OfferedAnswerQuestionRelation } from 'src/app/models/offeredAnswerQuestionRelation';



@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private readonly httpClient: HttpClient) { }

  getSurvey(surveyId): Observable<SurveyResponse> {
    return this.httpClient.get<SurveyResponse>(`${environment.apiUrl}/api/Survey/${surveyId}`);
  }

getSurveyResult(surveyId): Observable<any> {
    return this.httpClient.get<any>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }

  getSurveyAnswers(surveyId): Observable<OfferedAnswers> {
    return this.httpClient.get<OfferedAnswers>(`${environment.apiUrl}/api/Survey/${surveyId}/OfferedAnswers`);
  }
  addSurveyResult(answer:Answer): Observable<Answer> {
   return this.httpClient.post<Answer>(`${environment.apiUrl}/api/Survey/Answers`,answer);

  }

  getOfferedAnswers(): Observable<OfferedAnswerOriginal[]> {
    return this.httpClient.get<OfferedAnswerOriginal[]>(`${environment.apiUrl}/api/Survey/OfferedAnswers`);
  
   }
   addSurvey(survey:SurveyResponse): Observable<SurveyResponse> {
    return this.httpClient.post<SurveyResponse>(`${environment.apiUrl}/api/Survey/AddSurvey`,survey);
 
   }
   addOfferedAnswerQuestionRelation(offeredAnswerQuestionRelation:OfferedAnswerQuestionRelation): Observable<OfferedAnswerQuestionRelation> {
    return this.httpClient.post<OfferedAnswerQuestionRelation>(`${environment.apiUrl}/api/Survey/OfferedAnswerRelation`,offeredAnswerQuestionRelation);
 
   }
 


 
}
