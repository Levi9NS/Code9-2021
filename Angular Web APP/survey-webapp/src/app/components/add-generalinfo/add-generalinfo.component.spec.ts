import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGeneralinfoComponent } from './add-generalinfo.component';

describe('AddGeneralinfoComponent', () => {
  let component: AddGeneralinfoComponent;
  let fixture: ComponentFixture<AddGeneralinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddGeneralinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGeneralinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
