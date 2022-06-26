using System;
using System.Text;
using System.Security.Cryptography;

namespace Infrastructure.Utils
{
    internal static class PasswordUtils
    {
        private static byte[] _saltPass = Encoding.UTF8.GetBytes("212111996");

        internal static byte[] GeneratePassword(string plainText)
        {
            var plainTextConvert = Encoding.UTF8.GetBytes(plainText);
            byte[] saltedValue = plainTextConvert.Concat(_saltPass).ToArray();
            return new SHA256Managed().ComputeHash(saltedValue);
        }

        internal static bool ComparePassword(byte[] passwordDb, string password)
        {
            byte[] passwordHash = GeneratePassword(password);
            return passwordDb.SequenceEqual(passwordHash);
        }


    }
}

