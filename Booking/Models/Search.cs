namespace Booking.Models
{
    public class Search
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateOnly FlightDate { get; set;}
        public DateOnly ReturnDate { get; set; }
    }
}
