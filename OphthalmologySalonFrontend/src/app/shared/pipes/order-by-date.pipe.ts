import { Pipe, PipeTransform } from '@angular/core';
import { visitReadDTO } from '../../core/models/visitReadDTO';

@Pipe({
  name: 'orderByDate'
})
export class OrderByDatePipe implements PipeTransform {

  transform(visits: visitReadDTO[]): visitReadDTO[] {
    return visits.sort((a, b) => a.start.getTime() - b.start.getTime());
  }

}
