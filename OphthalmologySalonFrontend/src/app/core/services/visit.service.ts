import { Injectable } from '@angular/core';
import { visitCreateDTO } from '../models/visitCreateDTO';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VisitService {

  constructor(private http: HttpClient) { }

  public bookVisit = (route: string, body: visitCreateDTO) => {
    return this.http.post(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
