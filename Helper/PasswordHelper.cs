using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Helper
{
    internal class PasswordHelper
    {
        // Genera Key y IV desde una cadena maestra
        private static void GetAesKeyAndIV(string secret, out byte[] key, out byte[] iv)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] fullHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(secret));

                key = new byte[32]; // 256 bits
                iv = new byte[16]; // 128 bits

                Array.Copy(fullHash, 0, key, 0, 32);
                Array.Copy(fullHash, 0, iv, 0, 16);
            }
        }

        public static string Encrypt(string plainText, string secretKey)
        {
            GetAesKeyAndIV(secretKey, out byte[] key, out byte[] iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cs))
                {
                    writer.Write(plainText);
                    writer.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string secretKey)
        {
            GetAesKeyAndIV(secretKey, out byte[] key, out byte[] iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                byte[] buffer = Convert.FromBase64String(cipherText);

                using (var ms = new MemoryStream(buffer))
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
