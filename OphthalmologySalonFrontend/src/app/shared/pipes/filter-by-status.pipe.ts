import { Pipe, PipeTransform } from '@angular/core';
import { visitReadDTO } from '../../core/models/visitReadDTO';

@Pipe({
  name: 'filterByStatus'
})
export class FilterByStatusPipe implements PipeTransform {

  transform(visits: visitReadDTO[], status: string): visitReadDTO[] {
    return visits.filter((visit) => visit.visitStatus === status);
  }

}
