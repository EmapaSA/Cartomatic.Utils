using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Cartomatic.Utils.Crypto
{
    /// <summary>
    /// Ported from GeoMind utils...
    /// </summary>
    public partial class SymmetricEncryption
    {

#pragma warning disable 1591
        public enum Algorithm
        {
            RC2,
            DES,
            TripleDES,
            Rijndael
        }
#pragma warning restore 1591

        /// <summary>
        /// Gets algorythm 
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        protected static SymmetricAlgorithm GetAlgorithm(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.RC2:
                    return RC2.Create();

                case Algorithm.DES:
                    return DES.Create();

                case Algorithm.TripleDES:
                    return TripleDES.Create();

                case Algorithm.Rijndael:
                default:
                    return Rijndael.Create();
            }
        }

        /// <summary>
        /// Encrypts an incoming string using the provided key
        /// </summary>
        /// <param name="clear"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string clear, string key)
        {
            //make sure clearText is a string otherwise just return whatever has been passed
            if (string.IsNullOrEmpty(clear)) return clear;

            // First we need to turn the input string into a byte array. 
            var clearBytes = System.Text.Encoding.Unicode.GetBytes(clear);

            //Get PasswordDeriveBytes
            var pdb = GetPasswordDeriveBytes(key);

            return EncryptString(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16), Algorithm.Rijndael);
        }

        /// <summary>
        /// Encrypts a file
        /// </summary>
        /// <param name="clearBytes"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] EncryptFile(byte[] clearBytes, string key)
        {
            if (clearBytes == null || clearBytes.Length == 0) return clearBytes;

            //Get PasswordDeriveBytes
            var pdb = GetPasswordDeriveBytes(key);

            return Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16), Algorithm.Rijndael);
        }

        /// <summary>
        /// Decypts an incoming string using provided key
        /// </summary>
        /// <param name="cypher"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cypher, string key)
        {
            //make sure clearText is a string otherwise just return whatever has been passed
            if (string.IsNullOrEmpty(cypher)) return cypher;

            // First we need to turn the input string into a byte array. 
            // We presume that Base64 encoding was used 
            var cipherBytes = Convert.FromBase64String(cypher);

            var pdb = GetPasswordDeriveBytes(key);

            return DecryptString(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16), Algorithm.Rijndael);
        }

        /// <summary>
        /// Descrypts a file
        /// </summary>
        /// <param name="cypherBytes"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] DecryptFile(byte[] cypherBytes, string key)
        {
            if (cypherBytes == null || cypherBytes.Length == 0) return cypherBytes;

            //Get PasswordDeriveBytes
            var pdb = GetPasswordDeriveBytes(key);

            return Decrypt(cypherBytes, pdb.GetBytes(32), pdb.GetBytes(16), Algorithm.Rijndael);
        }

        /// <summary>
        /// Gets password derived bytes based on... well password ;-)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static PasswordDeriveBytes GetPasswordDeriveBytes(string password)
        {
            //turn input into bytes - make sure the pass is hashed before making it salt
            var salt = System.Text.Encoding.Unicode.GetBytes(Hasher.ComputeShaHash(password, 1));

            salt.Reverse();

            return new PasswordDeriveBytes(password, salt);
        }


        /// <summary>
        /// Encrypt a byte array into a byte array using a key and an IV 
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV, Algorithm algorithm)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            var ms = new MemoryStream();

            // Create a symmetric algorithm. 
            var alg = GetAlgorithm(algorithm);


            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining).
            // The IV is XORed with the first block (8 byte) 
            // of the data before it is encrypted, and then each
            // encrypted block is XORed with the 
            // following block of plaintext.
            // This is done to make encryption more secure. 

            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream and the output will be written
            // in the MemoryStream we have provided. 
            var cs = new CryptoStream(ms,
                alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our encryption and
            // there is no more data coming in, 
            // and it is now a good time to apply the padding and
            // finalize the encryption process. 
            cs.Close();

            // Now get the encrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            var encryptedData = ms.ToArray();

            return encryptedData;
        }

        /// <summary>
        /// encrypts a string
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string EncryptString(byte[] clearData, byte[] Key, byte[] IV, Algorithm algorithm)
        {
            return Convert.ToBase64String(Encrypt(clearData, Key, IV, algorithm));
        }


        /// <summary>
        /// Decrypt a byte array into a byte array using a key and an IV 
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV, Algorithm algorithm)
        {
            // Create a MemoryStream that is going to accept the
            // decrypted bytes 
            var ms = new MemoryStream();

            // Create a symmetric algorithm. 
            var alg = GetAlgorithm(algorithm);

            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm
            // is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with
            // the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted
            // block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be
            // pumping our data. 
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream 
            // and the output will be written in the MemoryStream
            // we have provided. 
            var cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption
            // and there is no more data coming in, 
            // and it is now a good time to remove the padding
            // and finalize the decryption process. 
            cs.Close();

            // Now get the decrypted data from the MemoryStream. 
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way. 
            var decryptedData = ms.ToArray();

            return decryptedData;
        }

        /// <summary>
        /// decrypt string
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string DecryptString(byte[] cipherData, byte[] Key, byte[] IV, Algorithm algorithm)
        {
            return System.Text.Encoding.Unicode.GetString(Decrypt(cipherData, Key, IV, algorithm));
        }
    }
}
