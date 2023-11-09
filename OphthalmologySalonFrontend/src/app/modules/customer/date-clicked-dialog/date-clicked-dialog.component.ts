import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { VisitService } from '../../../core/services/visit.service';
import { visitCreateDTO } from '../../../core/models/visitCreateDTO';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-date-clicked-dialog',
  templateUrl: './date-clicked-dialog.component.html',
  styleUrls: ['./date-clicked-dialog.component.css']
})
export class DateClickedDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DateClickedDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private visitService: VisitService
  ) { }

  onCancelClick(): void {
    this.dialogRef.close(false);
  }

  onConfirmClick(): void {
    this.dialogRef.close(true);

    const visit: visitCreateDTO = {
      start: this.data.event.start,
      visitType: this.data.visitType
    };

    this.visitService.bookVisit("api/v1/Customer/Visit", visit).subscribe({
      next: () => {
        console.log("visit succesfully booked");
      },
      error: (err: HttpErrorResponse) => {
        console.log(err.error + " " + err.message);
      }
    });
  }
}
