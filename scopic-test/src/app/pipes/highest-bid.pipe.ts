import { Pipe, PipeTransform } from '@angular/core';
import { GetHighestBid } from '../helpers/transform';

@Pipe({
  name: 'highestBid'
})
export class HighestBidPipe implements PipeTransform {

  transform(value: any, ...args: unknown[]): any {
    
    return GetHighestBid(value);;
  }

}
