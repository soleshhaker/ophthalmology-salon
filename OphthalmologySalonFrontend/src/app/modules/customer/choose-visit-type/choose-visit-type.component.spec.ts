import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseVisitTypeComponent } from './choose-visit-type.component';

describe('ChooseVisitTypeComponent', () => {
  let component: ChooseVisitTypeComponent;
  let fixture: ComponentFixture<ChooseVisitTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChooseVisitTypeComponent]
    });
    fixture = TestBed.createComponent(ChooseVisitTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
