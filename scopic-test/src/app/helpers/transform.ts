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
        return newDate;
      }

      export function GetDateDifferenceInSeconds(date:Date){
        console.log("WEW")
        var expiryDate = ConvertToLocalTime(date);
        // var currentDate = new Date();
        var seconds = (expiryDate.getTime() / 1000);
        return seconds;
      }
