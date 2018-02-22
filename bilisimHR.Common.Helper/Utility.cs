using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace bilisimHR.Common.Helper
{
    public static class Utility
    {
        public static readonly string AuthCookieName = "bilisimHR.API.Authentication";
        public static readonly string AuthenticationType = "ApplicationCookie";

        public static bool IsList(object o)
        {
            return o is IList && o.GetType().IsGenericType && o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static void SaltPassword(string password, out string salt, out string saltedPassword)
        {
            var saltVal = generateSalt(6);
            var saltedPassVal = generatedSaltedHash(Encoding.UTF8.GetBytes(password), saltVal);
            salt = Convert.ToBase64String(saltVal);
            saltedPassword = Convert.ToBase64String(saltedPassVal);
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] plainText, byte[] salt)
        {
            //byte[] saltedValue = value.Concat(salt).ToArray();
            // Alternatively use CopyTo.
            //var saltedValue = new byte[value.Length + salt.Length];
            //value.CopyTo(saltedValue, 0);
            //salt.CopyTo(saltedValue, value.Length);

            //return new SHA256Managed().ComputeHash(saltedValue);
            return generatedSaltedHash(plainText, salt);
        }

        private static byte[] generatedSaltedHash(byte[] plainText, byte[] salt)
        {
            try
            {
                HashAlgorithm algorithm = new SHA256Managed();

                byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                return algorithm.ComputeHash(plainTextWithSaltBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static byte[] generateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[saltSize];
            rng.GetBytes(buff);
            return buff;
        }
    }
}
