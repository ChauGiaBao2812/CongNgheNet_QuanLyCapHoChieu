using System.Security.Cryptography;
using System.Text;

namespace QuanLiHoChieu.Helpers
{
    public static class AesEcbEncryption
    {
        public static byte[] EncryptAesEcb(string plaintext, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);
                return encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            }
        }

        public static string DecryptAesEcb(byte[] encryptedBytes, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}