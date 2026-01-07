using System;
using System.Security.Cryptography;


namespace Cartomatic.Utils.Crypto
{
    /// <summary>
    /// Simple hasher
    /// </summary>
    public class Hasher
    {
#pragma warning disable 1591
        public enum Algorythm
        {
            Sha1 = 1,
            Sha256 = 256,
            Sha512 = 512
        }
#pragma warning restore 1591


        /// <summary>
        /// Hashes a string using one of the sha algorithms - 1, 256 or 512
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="shaVersion">Version of the sha algorythm - Sha1, Sha256, Sha512; defaults to Sha1</param>
        /// <param name="useBase64Encoding">Whether or not use base64 encoding for a returned string</param>
        /// <returns>Hashed string</returns>
        public static string ComputeShaHash(string s, Algorythm shaVersion, bool useBase64Encoding = false)
        {
            return ComputeShaHash(s, (int) shaVersion, useBase64Encoding);
        }

        /// <summary>
        /// Hashes a string using one of the sha algorithms - 1, 256 or 512
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="shaVersion">Version of the sha algorythm - Sha1, Sha256, Sha512; defaults to Sha1</param>
        /// <param name="useBase64Encoding">Whether or not use base64 encoding for a returned string</param>
        /// <returns>Hashed string</returns>
        public static string ComputeShaHash(string s, int shaVersion, bool useBase64Encoding = false)
        {
            //get an array of bytes off the string
            byte[] sb = System.Text.Encoding.UTF8.GetBytes(s);

            //container for hashed string
            byte[] ob;

            switch (shaVersion)
            {
                case 1:
                default:
                    using (SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider())
                    {
                        ob = sha.ComputeHash(sb);
                    }
                    break;

                case 256:
                    using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
                    {
                        ob = sha.ComputeHash(sb);
                    }
                    break;

                case 512:
                    using (SHA512CryptoServiceProvider sha = new SHA512CryptoServiceProvider())
                    {
                        ob = sha.ComputeHash(sb);
                    }
                    break;
            }

            if (useBase64Encoding)
            {
                return Convert.ToBase64String(ob);
            }
            else
            {
                //remove the dashes and make string lowercase
                return BitConverter.ToString(ob).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// computes an md5 hash
        /// </summary>
        /// <param name="s"></param>
        /// <param name="useBase64Encoding"></param>
        /// <returns></returns>
        public static string ComputeMd5Hash(string s, bool useBase64Encoding = false)
        {
            //get an array of bytes off the string
            byte[] sb = System.Text.Encoding.UTF8.GetBytes(s);

            //container for hashed string
            byte[] ob;

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                ob = md5.ComputeHash(sb);
            }

            if (useBase64Encoding)
            {
                return Convert.ToBase64String(ob);
            }
            else
            {
                //remove the dashes and make string lowercase
                return BitConverter.ToString(ob).Replace("-", "").ToLower();
            }
        }
    }
}
