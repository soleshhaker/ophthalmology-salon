import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { parseISO, format } from 'date-fns'; // Import a date parsing library like date-fns
import { CalendarEvent, CalendarView } from 'angular-calendar';

@Component({
  selector: 'app-choose-visit-type',
  templateUrl: './choose-visit-type.component.html',
  styleUrls: ['./choose-visit-type.component.css']
})
export class ChooseVisitTypeComponent {
  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date(); // Set the initial view date
  availableEvents: CalendarEvent[] = [];


  availableTimes: MonthlyAvailableTimes[] = [];
  currentMonthIndex: number = 0;
  visibleMonth: MonthlyAvailableTimes | null = null;

  constructor(private http: HttpClient) {}

  handleButtonClick(visitType: string) {
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
    // Replace the URL with your actual backend endpoint
    const backendUrl = `https://localhost:7105/Api/v1/Customer/Visit/AvailableTime?visitType=${visitType}`;
    return this.http.get<string[]>(backendUrl);
  }

  organizeAvailableTimes(times: string[]) {
    const organizedData: { [key: string]: MonthlyAvailableTimes } = {};

    times.forEach((time) => {
      const date = parseISO(time); // Parse the date string into a JavaScript Date object
      const key = this.getMonthYearKey(date);
      if (!organizedData[key]) {
        organizedData[key] = { month: date, times: [] };
      }
      organizedData[key].times.push(format(date, 'HH:mm')); // Format the time
    });

    this.availableTimes = Object.values(organizedData);
  }

  handleEventClicked(event: CalendarEvent): void {
    // Handle the event click as needed
  }

  navigateToMonth(offset: number) {
    const newMonthIndex = this.currentMonthIndex + offset;
    if (newMonthIndex >= 0 && newMonthIndex < this.availableTimes.length) {
      this.currentMonthIndex = newMonthIndex;
      this.visibleMonth = this.availableTimes[this.currentMonthIndex];
    }
  }

  getMonthYearKey(date: Date): string {
    return date.getMonth() + '-' + date.getFullYear();
  }

}

interface MonthlyAvailableTimes {
  month: Date; // Date representing the month
  times: string[]; // Array of available times for that month
}

