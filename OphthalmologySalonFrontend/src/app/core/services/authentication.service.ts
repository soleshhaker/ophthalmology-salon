import { RegisterUserDTO } from '../models/registerUserDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoginUserDTO } from '../models/loginUserDTO';
import { AuthResponseDto } from '../models/authResponseDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  constructor(private http: HttpClient) { }

  public registerUser = (route: string, body: RegisterUserDTO) => {
    return this.http.post<boolean>(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  public loginUser = (route: string, body: LoginUserDTO) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, 'https://localhost:7105'), body);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}