import { Pipe, PipeTransform } from '@angular/core';
import { ConvertToLocalTime } from '../helpers/transform';

@Pipe({
  name: 'toLocalTime'
})
export class ToLocalTimePipe implements PipeTransform {

  transform(value: Date, ...args: unknown[]): unknown {
    
    return ConvertToLocalTime(value);;
  }

}
