import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChooseVisitTypeComponent } from './choose-visit-type/choose-visit-type.component';
import { RouterModule } from '@angular/router';
import { CalendarModule, CalendarUtils, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';


@NgModule({
  declarations: [
    ChooseVisitTypeComponent,
  ],
  imports: [
    CommonModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    RouterModule.forChild([
    { path: "choose-visit-type", component: ChooseVisitTypeComponent }
    ])
  ],
  providers: [
    CalendarUtils
  ]
})
export class CustomerModule { }
