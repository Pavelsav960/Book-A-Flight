namespace Booking.Models;
using System;
using System.Linq;

public class CardValidator
{
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
