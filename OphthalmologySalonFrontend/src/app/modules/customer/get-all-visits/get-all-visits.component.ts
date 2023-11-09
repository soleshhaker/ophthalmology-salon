import { Component, OnInit } from '@angular/core';
import { VisitService } from '../../../core/services/visit.service';
import { visitReadDTO } from '../../../core/models/visitReadDTO';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-get-all-visits',
  templateUrl: './get-all-visits.component.html',
  styleUrls: ['./get-all-visits.component.css']
})
export class GetAllVisitsComponent implements OnInit {

  visits: visitReadDTO[] = [];
  constructor(private visitService: VisitService) {

  }

  ngOnInit(): void {
    this.visits = [];
    this.visitService.customerGetAllVisits("api/v1/customer/visit/allvisits").subscribe({
      next: (response: visitReadDTO[]) => {
        this.visits = response.map(visit => ({
          ...visit,
          start: new Date(visit.start), // Convert start string to Date
          end: new Date(visit.end),     // Convert end string to Date
        }));
      },
      error: (err: HttpErrorResponse) => {
        console.log(err.error + " " + err.message);
      }
    })
  }

  getStatuses(): string[] {
    return ["Pending", "Approved", "Rejected", "Completed"];
  }
}
