using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.Extensions
{
    public static class Encryption
    {
        private static readonly byte[] Vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public static string Encrypt(this string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var key = Encoding.UTF8.GetBytes("Gg!*123?");
                var inputArr = Encoding.UTF8.GetBytes(text);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, new DESCryptoServiceProvider().CreateEncryptor(key, Vector), CryptoStreamMode.Write);
                cs.Write(inputArr, 0, inputArr.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }

        public static string Decrypt(this string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var inputArr = Convert.FromBase64String(text);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, new DESCryptoServiceProvider().CreateDecryptor(Encoding.UTF8.GetBytes("Gg!*123?"), Vector), CryptoStreamMode.Write);
                cs.Write(inputArr, 0, inputArr.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray()).Replace(" ", "+").Trim();
            }
            catch
            {
                return "";
            }
        }
    }
}
