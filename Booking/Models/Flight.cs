using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class Flight
    {
        [Key]
        [Required]
        public int FlightId { get; set; }
        [Required]
        [Display(Name = "Origin Country")]
        public string OriginCountry { get; set; }
        [Required]
        [Display(Name = "Destination Country")]
        public string DestinationCountry { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Flight Date")]
        public DateTime FlightDate { get; set; } = DateTime.Now;
        [Required]
            
        public TimeSpan ExpireDate { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

        //public void SeatTaken()
        //{
        //    AvailableSeats -= 1;
        //}

    }
}
