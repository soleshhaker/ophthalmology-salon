import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FetchVisitsComponent } from './fetch-visits/fetch-visits.component';
import { LoginComponent } from './log-in/log-in.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterUserComponent } from './register-user/register-user.component';
import { AuthenticationContainerComponent } from './authentication-container/authentication-container.component';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from '../core/guards/auth.guard';

export function tokenGetter() {
  console.log("getting token");
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    FetchVisitsComponent,
    LoginComponent,
    RegisterUserComponent,
    AuthenticationContainerComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-visits', component: FetchVisitsComponent, canActivate: [AuthGuard] },
      { path: 'log-in', component: LoginComponent },
      { path: 'register', component: RegisterUserComponent },
      { path: 'auth', component: AuthenticationContainerComponent },

    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7105"],
        disallowedRoutes: [],
      }
    })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorHandlerService,
    multi: true
  },
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
