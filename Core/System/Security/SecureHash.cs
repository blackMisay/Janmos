using System;
using System.Text;
using System.Security.Cryptography;

namespace Core.System.Security
{
    internal static class SecureHash
    {
        /// <summary>
        /// This generates a random salt value using a secure random number 
        /// generator and returns it as a base64 encoded string.
        /// </summary>
        /// <returns>This salt can be used for password hashing</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }


        /// <summary>
        /// This accepts a password and a salt, combines them, generates a 
        /// SHA256 hash of the combination, and returns the hash as a base64-encoded string.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Concatenate the password and salt, then hash
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Convert the hashed bytes to a base64 string
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}