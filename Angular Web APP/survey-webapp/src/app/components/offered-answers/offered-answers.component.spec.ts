import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferedAnswersComponent } from './offered-answers.component';

describe('OfferedAnswersComponent', () => {
  let component: OfferedAnswersComponent;
  let fixture: ComponentFixture<OfferedAnswersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfferedAnswersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferedAnswersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
