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
        public string CardNumber { get; set; }

        [Display(Name = "Card expiry date(Month)")]
        public string ExparationMonth { get; set; }

        [Display(Name = "Card expiry date(Year)")]
        public string ExparationYear { get; set;}

        [Display(Name = "Save card Details ?")]
        public bool SaveDetails { get; set; }

        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return false;

            // Remove any non-digit characters from the card number
            cardNumber = new string(cardNumber.Where(char.IsDigit).ToArray());

            // Check if the card number is too short
            if (cardNumber.Length < 12)
                return false;

            // Check if the card number is too long
            if (cardNumber.Length > 19)
                return false;

            // Use the Luhn algorithm to check if the card number is valid
            int sum = cardNumber.Reverse().Select((c, i) => (c - '0') * (i % 2 == 0 ? 1 : 2))
                                .Sum((x) => x / 10 + x % 10);

            return sum % 10 == 0;
        }

    }
}
