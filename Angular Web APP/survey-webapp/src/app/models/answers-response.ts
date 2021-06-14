export class OfferedAnswers {
  public offeredAnswers: OfferedAnswer[];
}

export class OfferedAnswer {
  public questionId: string;
  public offeredAnswerId: string;
  public questionAnswer: string;
  public questionText: string;
}
