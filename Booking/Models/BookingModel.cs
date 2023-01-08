using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class BookingModel
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int DepartingFlightId { get; set; }
        public int ReturnFlightId { get; set; } = -1;
        public int DepartingSeatNumber { get; set; }
        [Required]
        public string BookingEmail { get; set; }


    }
}
