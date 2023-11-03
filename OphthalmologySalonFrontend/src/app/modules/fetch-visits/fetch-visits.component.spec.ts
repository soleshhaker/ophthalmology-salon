import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchVisitsComponent } from './fetch-visits.component';

describe('FetchVisitsComponent', () => {
  let component: FetchVisitsComponent;
  let fixture: ComponentFixture<FetchVisitsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FetchVisitsComponent]
    });
    fixture = TestBed.createComponent(FetchVisitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
