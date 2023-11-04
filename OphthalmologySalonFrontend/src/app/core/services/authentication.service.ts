import { RegisterUserDTO } from '../models/registerUserDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoginUserDTO } from '../models/loginUserDTO';
import { AuthResponseDto } from '../models/authResponseDTO';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

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

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  public isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");

    // Check if token is null or undefined
    if (!token) {
      return false;
    }

    const decodedToken = this.jwtHelper.decodeToken(token);

    // Check if decodedToken is null or undefined, and if the role property exists
    if (!decodedToken || !decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']) {
      return false;
    }

    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    console.log(role);
    return role === 'Admin';
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
