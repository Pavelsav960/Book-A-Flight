using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Booking.Models
{
    public class Search
    {
        public string From { get; set; }
        public string To { get; set; }

        [Display(Name = "Departure:")]
        public DateTime FlightDate { get; set;}
        [Display(Name = "Return:")]
        public DateTime ReturnDate { get; set; }
        public bool isOneWay { get; set; }

        [Display(Name = "Min Price:")]
        public int PriceMin { get; set; }
        [Display(Name = "Max Price:")]
        public int PriceMax { get; set; }
        public int NumOfTickets { get; set; }

    }
}
