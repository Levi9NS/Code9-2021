import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOfferedAnswerComponent } from './add-offered-answer.component';

describe('AddOfferedAnswerComponent', () => {
  let component: AddOfferedAnswerComponent;
  let fixture: ComponentFixture<AddOfferedAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOfferedAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOfferedAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
