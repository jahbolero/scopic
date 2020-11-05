import { Pipe, PipeTransform } from '@angular/core';
import { ConvertToLocalTime } from '../helpers/transform';

@Pipe({
  name: 'timerValue'
})
export class TimerValuePipe implements PipeTransform {

  transform(date: Date, ...args: unknown[]): unknown {
    var expiryDate = ConvertToLocalTime(date);
    var currentDate = new Date();
    var seconds = (expiryDate.getTime() - currentDate.getTime()) / 1000;
    return seconds;
  }

}
