import { RegisterUserDTO } from '../models/registerUserDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  public registerUser = (route: string, body: RegisterUserDTO) => {
    return this.http.post<boolean>(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  public login(route: string, username: string, password: string): Observable<boolean> {
    const body = { username, password };
    return this.http.post<boolean>(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
