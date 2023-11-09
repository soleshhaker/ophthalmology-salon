import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-date-confirmed-dialog',
  templateUrl: './date-confirmed-dialog.component.html',
  styleUrls: ['./date-confirmed-dialog.component.css']
})
export class DateConfirmedDialogComponent {

  constructor(public dialogRef: MatDialogRef<DateConfirmedDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,) { }

  onConfirmClick(): void {
    console.log(this.data);
    this.dialogRef.close(true);
  }
}
