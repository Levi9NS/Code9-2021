import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QuestionAndAnswers, SurveyResponse } from 'src/app/models/survey-response';
import { Participant } from 'src/app/models/participant-model';
import { GeneralInformations } from 'src/app/models/general-informations';
import { SurveyResults } from 'src/app/models/survey-result';
import { OfferedAnswers, OfferedAnswersModel } from 'src/app/models/answers-response';

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

  getSurveyResults(surveyId) : Observable<SurveyResults>{
    return this.httpClient.get<SurveyResults>(`${environment.apiUrl}/api/Survey/${surveyId}/Answers`);
  }

  getAllSurveys(): Observable<Array<GeneralInformations>>{
    return this.httpClient.get<Array<GeneralInformations>>(`${environment.apiUrl}/api/Survey/GetAllGeneralInformations`);
  }

  getAllOfferedAnswers(){
    return this.httpClient.get<Array<OfferedAnswersModel>>(`${environment.apiUrl}/api/Survey/GetOfferedAnswers`);
  }

  addParticipant(participant: any) {
    
    return this.httpClient.post<any>(`${environment.apiUrl}/api/Survey/participant/Add`, participant);
  }

  addGeneralInformations(generalinfo : GeneralInformations){
    return this.httpClient.post<GeneralInformations>(`${environment.apiUrl}/api/Survey/generalInformations/Add`, generalinfo);
  }

  addQuestion(questionAndAnswers: QuestionAndAnswers){
    return this.httpClient.post<QuestionAndAnswers>(`${environment.apiUrl}/api/Survey/questionAndAnswers/Add`, questionAndAnswers);
  }

}
