import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAnswersComponent } from './add-answers.component';

describe('AddAnswersComponent', () => {
  let component: AddAnswersComponent;
  let fixture: ComponentFixture<AddAnswersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAnswersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAnswersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
