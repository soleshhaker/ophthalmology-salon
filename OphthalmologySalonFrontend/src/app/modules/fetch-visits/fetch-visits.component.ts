import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-visits',
  templateUrl: './fetch-visits.component.html'
})
export class FetchVisitsComponent {
  public visits: Visit[] = [];

  constructor(http: HttpClient, @Inject('BACKEND_URL') baseUrl: string) {
    http.get<Visit[]>(baseUrl + 'api/v1/admin/visit').subscribe(result => {
      this.visits = result;
      console.log("check");
    }, error => console.error(error));
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
