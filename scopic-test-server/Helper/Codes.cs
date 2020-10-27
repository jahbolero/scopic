using System.ComponentModel;

namespace scopic_test_server.Helper
{
    public static class Codes
    {
        public enum BidCode
        {
            Success,
            [Description("You can't make a bid if you're the current highest bidder.")]
            HighestBid,//Currently the highest bidder
            [Description("Your bid price is lower than the current bid price.")]
            PriceTooLow,//Bid price lower than highest bidder
            [Description("Something went wrong. Please try again.")]
            Null,//
        }
    }

}