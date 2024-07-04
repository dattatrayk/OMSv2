using System;
using System.Security.Cryptography;
using System.Text;
using OMSv2.Service.Helpers;

namespace Fleetroot.Common.Helper
{
    public class EncryptionDecryptionHelper
    {
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            using (var hashmd5 = new MD5CryptoServiceProvider())
            {
                string encryptionKey = AppSettingProvider.GetEncryptionKey();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encryptionKey));
            }

            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        public static string Decrypt(string cipherString)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    string encryptionKey = AppSettingProvider.GetEncryptionKey();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
                }

                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
