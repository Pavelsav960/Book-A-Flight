using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class Payment
    {
        [Key]
        [Display(Name = "Card holder ID number")]
        public int IdNumber { get; set; }

        //[Display(Name = "Card holder first name")]
        //public string CardHolderFirstName { get; set; }

        //[Display(Name = "Card holder full name")]
        //public string CardHolderLastName { get; set;}

        [Display(Name = "Card holder Email address")]
        public string Email { get; set; }

        [Display(Name = "Card holder full name")]
        public string CardHolderName { get; set; }

        [Display(Name = "Card number")]
        public int CardNumber { get; set; }

        [Display(Name = "Card expiry date(Month)")]
        public string ExparationMonth { get; set; }

        [Display(Name = "Card expiry date(Year)")]
        public string ExparationYear { get; set;}

        [Display(Name = "Save card Details ?")]
        public bool SaveDetails { get; set; }

    }
}
