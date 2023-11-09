import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChooseVisitTypeComponent } from './choose-visit-type/choose-visit-type.component';
import { RouterModule } from '@angular/router';
import { CalendarModule, CalendarUtils, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { DateClickedDialogComponent } from './date-clicked-dialog/date-clicked-dialog.component';
import { DateConfirmedDialogComponent } from './date-confirmed-dialog/date-confirmed-dialog.component';
import { GetAllVisitsComponent } from './get-all-visits/get-all-visits.component';
import { FilterByStatusPipe } from '../../shared/pipes/filter-by-status.pipe'
import { OrderByDatePipe } from '../../shared/pipes/order-by-date.pipe'

@NgModule({
  declarations: [
    ChooseVisitTypeComponent,
    DateClickedDialogComponent,
    DateConfirmedDialogComponent,
    GetAllVisitsComponent,
    FilterByStatusPipe,
    OrderByDatePipe
  ],
  imports: [
    CommonModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    RouterModule.forChild([
      { path: "choose-visit-type", component: ChooseVisitTypeComponent },
      { path: "get-all-visits", component: GetAllVisitsComponent }

    ])
  ],
  providers: [
    CalendarUtils
  ]
})
export class CustomerModule { }
