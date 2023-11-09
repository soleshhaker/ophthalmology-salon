import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateConfirmedDialogComponent } from './date-confirmed-dialog.component';

describe('DateConfirmedDialogComponent', () => {
  let component: DateConfirmedDialogComponent;
  let fixture: ComponentFixture<DateConfirmedDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DateConfirmedDialogComponent]
    });
    fixture = TestBed.createComponent(DateConfirmedDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
