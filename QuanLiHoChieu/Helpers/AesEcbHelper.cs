using System.Security.Cryptography;
using System.Text;

namespace QuanLiHoChieu.Helpers
{
    public static class AesEcbEncryption
    {
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("your-32-char-key-1234567890abcde");
        public static byte[] EncryptAesEcb(string plaintext)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);
                return encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            }
        }

        public static string? DecryptAesEcb(byte[]? encryptedBytes)
        {
            if (encryptedBytes == null || encryptedBytes.Length == 0)
                return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}