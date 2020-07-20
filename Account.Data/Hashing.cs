using System;
using System.Security.Cryptography;
using System.Text;

namespace Account.Data
{
    public static class Hashing
    {
        public static string GetSalt()
        {
            var random = new RNGCryptoServiceProvider();
            int maxLength = 32;
            byte[] salt = new byte[maxLength];
            random.GetNonZeroBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
