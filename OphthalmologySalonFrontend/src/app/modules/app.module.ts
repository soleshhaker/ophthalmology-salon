import { HttpClientModule } from '@angular/common/http';
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
      { path: 'fetch-visits', component: FetchVisitsComponent },
      { path: 'log-in', component: LoginComponent },
      { path: 'register', component: RegisterUserComponent },
      { path: 'auth', component: AuthenticationContainerComponent },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
