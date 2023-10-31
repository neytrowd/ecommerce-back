using System;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Common.Utils
{
    public static class HashUtil
    {
        public static bool VerifyEquality(string hashedValue, string value)
        {
            var hash = Hash(value);
            return hashedValue == hash;
        }

        public static string Hash(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var md5 = new MD5CryptoServiceProvider();
            var hashed = md5.ComputeHash(data);
            return Convert.ToBase64String(hashed);
        }
    }
}