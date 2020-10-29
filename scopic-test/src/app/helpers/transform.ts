import { Bid } from '../models/bid';


      export function GetHighestBid(bid:Array<Bid> )
      {
        let highest = bid.reduce((max, bid) => {
        return bid.bidAmount >= max.bidAmount ? bid : max;
      });
      return highest;
      }
      export function ConvertToLocalTime(date:Date){
        var newDate = new Date(`${date.toString()}Z`);
        console.log(newDate);
        return newDate;
      }

