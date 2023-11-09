import { HttpClient } from '@angular/common/http';
import { Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { parseISO, format, addMonths, isBefore, isSameMonth, isAfter, isSameDay } from 'date-fns'; // Import a date parsing library like date-fns
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { DateClickedDialogComponent } from '../date-clicked-dialog/date-clicked-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-choose-visit-type',
  templateUrl: './choose-visit-type.component.html',
  styleUrls: ['./choose-visit-type.component.css']
})

export class ChooseVisitTypeComponent implements OnInit {
  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  availableEvents: CalendarEvent[] = [];

  activeDayIsOpen: boolean = false;

  availableTimes: MonthlyAvailableTimes[] = [];
  currentMonthIndex: number = 0;
  visibleMonth: MonthlyAvailableTimes | null = null;

  selectedVisitType: string = "";
  constructor(private http: HttpClient, private dialog: MatDialog) { }


  ngOnInit() {
    this.handleButtonClick('RoutineEyeExam');
  }


  handleButtonClick(visitType: string) {

    this.availableEvents = [];
    this.currentMonthIndex = 0;
    this.activeDayIsOpen = false;

    // Fetch available times from the backend for the selected visit type
    this.fetchAvailableTimes(visitType).subscribe(
      (data: string[]) => {
        // Organize the available times by month
        this.organizeAvailableTimes(data);
      },
      (error: any) => {
        console.error('Error:', error);
      }
    );
  }

  fetchAvailableTimes(visitType: string): Observable<string[]> {
    const backendUrl = `https://localhost:7105/Api/v1/Customer/Visit/AvailableTime?visitType=${visitType}`;
    this.selectedVisitType = visitType;
    return this.http.get<string[]>(backendUrl);
  }

  organizeAvailableTimes(times: string[]) {
    const organizedData: { [key: string]: MonthlyAvailableTimes } = {};

    times.forEach((time) => {
      const date = parseISO(time); // Parse the date string into a JavaScript Date object
      const key = this.getMonthYearKey(date);
      if (!organizedData[key]) {
        organizedData[key] = { month: date, times: [], count: 0 };
      }
      organizedData[key].times.push(format(date, 'HH:mm')); // Format the time
      organizedData[key].count++; // Increment the count of available times for that day

      // Create a calendar event for this time slot
      const event: CalendarEvent = {
        start: date,
        title: 'Available',
        allDay: false,
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      };

      // Add the event to the available events
      this.availableEvents.push(event);
    });

    this.availableTimes = Object.values(organizedData);
    this.viewDate = new Date();
  }

  handleEventClick(event: CalendarEvent) {
    const dialogRef = this.dialog.open(DateClickedDialogComponent, {
      data: { event: event, visitType: this.selectedVisitType },
      height: '200px',
      width: '300px',
      panelClass: 'bg-color'
    });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  navigateToMonth(offset: number) {
    const currentDate = new Date();
    const newDate = addMonths(this.viewDate, offset);

    //TODO why is it so complex? it surely can be done more simply
    if (
      (offset > 0 && isBefore(newDate, addMonths(currentDate, 3))) || // Check if the newDate is within the next 3 months
      (offset < 0 && (isSameMonth(newDate, currentDate) || isAfter(newDate, currentDate))) // Allow going back to the current month but not beyond
    ) {
      this.activeDayIsOpen = false;
      this.currentMonthIndex += offset;
      this.viewDate = newDate;
    }
  }

  getAvailableTimesCount(date: Date): number {
    const key = this.getMonthYearKey(date);
    const availableTimes = this.availableTimes.find(times => times.month.getTime() === date.getTime());
    return availableTimes ? availableTimes.count : 0;
  }

  getMonthYearKey(date: Date): string {
    return date.getMonth() + '-' + date.getFullYear();
  }

}

interface MonthlyAvailableTimes {
  month: Date; // Date representing the month
  times: string[]; // Array of available times for that month
  count: number; // Count of available times for that day
}

