using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Booking.Models
{
    public class EncryptionClass
    {
        public static string EncryptCardNumber(string cardNumber, string encryptionKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] cardNumberBytes = Encoding.UTF8.GetBytes(cardNumber);
            byte[] encryptedCardNumberBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cardNumberBytes, 0, cardNumberBytes.Length);
                    }

                    encryptedCardNumberBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedCardNumberBytes);
        }
        public static string DecryptCardNumber(string encryptedCardNumber, string encryptionKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] encryptedCardNumberBytes = Convert.FromBase64String(encryptedCardNumber);
            byte[] decryptedCardNumberBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;

                using (MemoryStream ms = new MemoryStream(encryptedCardNumberBytes))
                {
                    byte[] iv = new byte[aes.BlockSize / 8];
                    ms.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        decryptedCardNumberBytes = new byte[encryptedCardNumberBytes.Length - iv.Length];
                        cs.Read(decryptedCardNumberBytes, 0, decryptedCardNumberBytes.Length);
                    }
                }
            }

            return Encoding.UTF8.GetString(decryptedCardNumberBytes);
        }
    }
}
