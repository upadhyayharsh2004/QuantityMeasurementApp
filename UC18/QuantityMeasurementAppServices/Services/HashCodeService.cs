using System.Text.RegularExpressions;

namespace QuantityMeasurementAppServices.Services
{
    public class HashCodeService
    {
        public string Hash(string HashPlainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(HashPlainText, workFactor: 11);
        }
        public bool Verify(string HashPlainText, string storedHashText)
        {
            return BCrypt.Net.BCrypt.Verify(HashPlainText, storedHashText);
        }
        public string HashSha256(string hassedInput)
        {
            using (System.Security.Cryptography.SHA256 hashText = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytesText = System.Text.Encoding.UTF8.GetBytes(hassedInput);
                byte[] hashBytesText  = hashText.ComputeHash(inputBytesText);
                return System.Convert.ToHexString(hashBytesText).ToLower();
            }
        }
    }
}