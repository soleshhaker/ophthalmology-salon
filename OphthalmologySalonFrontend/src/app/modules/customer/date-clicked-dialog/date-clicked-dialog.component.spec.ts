import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateClickedDialogComponent } from './date-clicked-dialog.component';

describe('DateClickedDialogComponent', () => {
  let component: DateClickedDialogComponent;
  let fixture: ComponentFixture<DateClickedDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateClickedDialogComponent]
    });
    fixture = TestBed.createComponent(DateClickedDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
