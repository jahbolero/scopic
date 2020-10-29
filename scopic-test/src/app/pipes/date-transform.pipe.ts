import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'unchacheDate'
})
export class DateTransformPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    var unchachedDate = `${value}?${new Date().getTime()}` 
    return unchachedDate;
  }

}
