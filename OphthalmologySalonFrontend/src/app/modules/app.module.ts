import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FetchVisitsComponent } from './fetch-visits/fetch-visits.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from '../core/guards/auth.guard';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AdminGuard } from '../core/guards/admin.guard';
import { ChooseVisitTypeComponent } from './choose-visit-type/choose-visit-type.component';
import { CalendarModule, CalendarUtils, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

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
    ForbiddenComponent,
    ChooseVisitTypeComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    ReactiveFormsModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-visits', component: FetchVisitsComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
      { path: "forbidden", component: ForbiddenComponent },
      { path: "choose-visit-type", component: ChooseVisitTypeComponent }

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
    CalendarUtils
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
