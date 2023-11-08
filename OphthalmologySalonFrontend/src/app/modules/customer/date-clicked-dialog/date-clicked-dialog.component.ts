import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-date-clicked-dialog',
  templateUrl: './date-clicked-dialog.component.html',
  styleUrls: ['./date-clicked-dialog.component.css']
})
export class DateClickedDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DateClickedDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  onCancelClick(): void {
    this.dialogRef.close(false);
  }

  onConfirmClick(): void {
    this.dialogRef.close(true);
  }
}
