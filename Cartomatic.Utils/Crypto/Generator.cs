using System;
using System.Security.Cryptography;

namespace Cartomatic.Utils.Crypto
{
    /// <summary>
    /// some crypto related generator helpers.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Generates random string
        /// </summary>
        /// <param name="length">Length of byte array to be used</param>
        /// <param name="useBase64Encoding">Whether or not use base64 encoding for the output</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length, bool useBase64Encoding)
        {
            //Create a random key using a random number generator
            var saltB = new byte[length];

            //RNGCryptoServiceProvider is an implementation of a random number generator. 
            using (var rng = new RNGCryptoServiceProvider())
            {
                //The array is now filled with cryptographically strong random bytes.
                rng.GetBytes(saltB);
            }

            return useBase64Encoding ? Convert.ToBase64String(saltB) : BitConverter.ToString(saltB).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Generates a random string of declared length
        /// </summary>
        /// <param name="length">Length of string to be returned</param>
        /// <returns>Random string</returns>
        public static string GenerateRandomString(int length)
        {
            return GenerateRandomString(length, false).Substring(0, length);
        }
    }
}
