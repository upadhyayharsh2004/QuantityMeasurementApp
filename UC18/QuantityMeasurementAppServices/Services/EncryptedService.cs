using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace QuantityMeasurementAppServices.Services
{
    public class EncryptedService
    {
        private readonly byte[] encryptedKey;

        public EncryptedService(IConfiguration encryptedConfiguration)
        {
            string encryptedkeyValue = encryptedConfiguration["Encryption:Key"]!;

            encryptedKey = new byte[32];
            byte[] encryptedkeyBytes = Encoding.UTF8.GetBytes(encryptedkeyValue);
            Array.Copy(encryptedkeyBytes, encryptedKey, Math.Min(encryptedkeyBytes.Length, encryptedKey.Length));
        }
        public string Encrypt(string hassedPlainText)
        {
            using (Aes hassedEncrypt = Aes.Create())
            {
                hassedEncrypt.Key = encryptedKey;
                hassedEncrypt.GenerateIV();
                byte[] hassedIv = hassedEncrypt.IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    
                    memoryStream.Write(hassedIv, 0, hassedIv.Length);

                    using (ICryptoTransform encryptorKey = hassedEncrypt.CreateEncryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptorKey, CryptoStreamMode.Write))
                    using (StreamWriter streamWriter= new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(hassedPlainText);
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        public string Decrypt(string cipherTextValue)
        {
            byte[] allDecryptedBytes = Convert.FromBase64String(cipherTextValue);

            using (Aes accessBytes = Aes.Create())
            {
                accessBytes.Key = encryptedKey;
                byte[] accessIv = new byte[accessBytes.BlockSize / 8];
                Array.Copy(allDecryptedBytes, 0, accessIv, 0, accessIv.Length);
                accessBytes.IV = accessIv;

                byte[] cipherDecryptedBytes = new byte[allDecryptedBytes.Length - accessIv.Length];
                Array.Copy(allDecryptedBytes, accessIv.Length, cipherDecryptedBytes, 0, cipherDecryptedBytes.Length);

                using (ICryptoTransform decryptorBytes = accessBytes.CreateDecryptor())
                using (MemoryStream memoryStream = new MemoryStream(cipherDecryptedBytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptorBytes, CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}