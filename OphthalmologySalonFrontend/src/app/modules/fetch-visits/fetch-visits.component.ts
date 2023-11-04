import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-visits',
  templateUrl: './fetch-visits.component.html'
})
export class FetchVisitsComponent {
  public visits: Visit[] = [];
  public errorMessage: string = '';
  public showError: boolean = false;
  constructor(http: HttpClient, @Inject('BACKEND_URL') baseUrl: string) {
    http.get<Visit[]>(baseUrl + 'api/v1/Admin/Visit').subscribe(
      result => {
        if (result && result.length > 0) {
          this.visits = result;
          this.showError = false;
        } else {
          this.errorMessage = "Cannot find any visits"; // Set the error message
          this.showError = true;
        }
      },
      error => {
        this.errorMessage = error.message; // Set an error message for other errors
        this.showError = true;
      }
    );
  }
}

interface Visit {
  date: string;
  id: number;
  start: string;
  end: string;
  applicationUser: string;
  applicationUserId: string;
  visitType: string;
  visitStatus: string;
  cost: number;
  additionalInfo: string;
}
