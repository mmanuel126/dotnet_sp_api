using System.Security.Cryptography;
using System.Text;
using dotnet_sp_api.Configurations;
using Microsoft.Extensions.Options;

namespace dotnet_sp_api.Helpers
{
    /// <summary>
    /// a simple cryptography implementation to encrypt and decrypt a string using DES
    /// DES (Data Ecryption Standard) is a symmetric-key algorithm used for encrypting digital data.
    /// This is an outdated but simple to implement for this project. Replace this to a stronger
    /// algorithm like AES for real world devevelopment.
    /// </summary>
    public class Crypto
    {
        private readonly byte[] _iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        private readonly byte[] _key;

        public Crypto(IOptions<EncryptionSettings> options)
        {
            var keyString = options.Value.EncryptionKey;
            _key = Encoding.UTF8.GetBytes(keyString);

            if (_key.Length != 8)
                throw new ArgumentException("DES key must be exactly 8 bytes long.");
        }

        public string Encrypt(string plainText)
        {
            try
            {
#pragma warning disable SYSLIB0021 // Type or member is obsolete
                using var des = new DESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);

                using var encryptor = des.CreateEncryptor(_key, _iv);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Convert.ToBase64String(encrypted);
            }
            catch
            {
                return "";
            }
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] encryptedBytes = Convert.FromBase64String(cipherText);
#pragma warning disable SYSLIB0021 // Type or member is obsolete
                using var des = new DESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                using var decryptor = des.CreateDecryptor(_key, _iv);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch
            {
                return "";
            }
        }
    }
}
