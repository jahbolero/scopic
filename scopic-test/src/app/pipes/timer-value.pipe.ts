import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timerValue'
})
export class TimerValuePipe implements PipeTransform {

  transform(timerValue: unknown, ...args: unknown[]): unknown {
    return 5;
  }

}
