import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'API_ENDPOINT'; 

  constructor(private http: HttpClient, @Inject('BACKEND_URL') baseUrl: string) {
    this.apiUrl = baseUrl;
  }

  login(email: string, password: string): Observable<boolean> {
    console.log(`${this.apiUrl}Login`);
    const body = { email, password };
    return this.http.post<boolean>(`${this.apiUrl}Login`, body);
  }
}
