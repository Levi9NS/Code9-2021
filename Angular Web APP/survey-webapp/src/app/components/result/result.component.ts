import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../../services/survey-service/survey-service.service';
import { SurveyResult } from '../../models/survey-result';

interface IGraph{
  name:string;
  value:number;
}

interface ISurveyGraphs{
  questionData: IQuestionData[];
}
interface IQuestionData{
  graph:IGraph[]
}

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {
  private result : SurveyResult;
  dataLoaded = false;
  private surveyGraphData: ISurveyGraphs;
  private view;
  surveyData: ISurveyGraphs;

  constructor(private readonly surveyService: SurveyService, private route: ActivatedRoute) {
    this.view = [innerWidth / 2.5, 300];
   }

  ngOnInit() {
    const surveyId = this.route.snapshot.params['id'];

    this.surveyService.getSurveyResults(surveyId).subscribe(answer => {
      this.result=answer;
      this.surveyData=this.getDataToGraph(this.result);
      this.dataLoaded = true;
    })
  }

  getDataToGraph(result:SurveyResult):ISurveyGraphs{
    var questionData:IQuestionData[] = result.questions.map(q=>{
      var graph :IGraph[] = q.answerResult.map(a=>{
        return{
          name:a.answer,
          value:a.count
        }
      })
      return{
        graph:graph
      }
    })

    return{
      questionData:questionData
    }
  }
  onResize(event) {
    this.view = [event.target.innerWidth / 2, 400];
}
 

}
