import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllVisitsComponent } from './get-all-visits.component';

describe('GetAllVisitsComponent', () => {
  let component: GetAllVisitsComponent;
  let fixture: ComponentFixture<GetAllVisitsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GetAllVisitsComponent]
    });
    fixture = TestBed.createComponent(GetAllVisitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
