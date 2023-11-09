import { Injectable } from '@angular/core';
import { visitCreateDTO } from '../models/visitCreateDTO';
import { HttpClient } from '@angular/common/http';
import { visitReadDTO } from '../models/visitReadDTO';

@Injectable({
  providedIn: 'root'
})
export class VisitService {

  constructor(private http: HttpClient) { }

  public bookVisit = (route: string, body: visitCreateDTO) => {
    return this.http.post(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  public customerGetAllVisits = (route: string) => {
    return this.http.get<visitReadDTO[]>(this.createCompleteRoute(route, 'https://localhost:7105'));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
