using System;
using System.Security.Cryptography;
using System.Text;

namespace Library
{
    public static class utility
    {
        public static string CalculateSHA(string userName, string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(userName + password);
            byte[] hashedData;
            SHA256 sHA256 = new SHA256Managed();
            string hashedStr;
            hashedData = sHA256.ComputeHash(data);
            hashedStr = BitConverter.ToString(hashedData).Replace("-", "");
            return hashedStr;
        }
    }
}