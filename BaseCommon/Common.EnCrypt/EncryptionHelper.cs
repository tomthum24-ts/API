using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.EnCrypt
{
    public static class EncryptionHelper
    {
        #region Property

        private static string _passEncrypt = "[aG]An%@-hG+ee2ky34@!";
        private static string _saltKey = "ij7[xsobI_rXulYjFv-c";
        private static string _VectorIV = "0By@GrXF_t6V[h!k";//"0By@GrXF_t6V[h!kWTg";

        private static CMSEncryption encrytion = new CMSEncryption(_passEncrypt, _saltKey, _VectorIV);

        #endregion Property

        #region Public Method

        public static string EncryptStr(string plainText)
        {
            try
            {
                plainText = EncodeURL(encrytion.Encrypt(plainText));
                return plainText;
            }
            catch
            {
                return null;
            }
        }

        public static string EncryptStr_2(string plainText)
        {
            try
            {
                plainText = EncodeURL(encrytion.Encrypt(plainText, _passEncrypt));
                return plainText;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string EncryptInt(int plainText)
        {
            try
            {
                string pText = EncodeURL(encrytion.Encrypt(plainText.ToString()));
                return pText;
            }
            catch
            {
                return null;
            }
        }

        public static string DecryptStr(string cipherText)
        {
            try
            {
                cipherText = encrytion.Decrypt(DecodeURL(cipherText)).Trim();
                return cipherText;
            }
            catch
            {
                return null;
            }
        }

        public static string DecryptStr_2(string cipherText)
        {
            try
            {
                cipherText = encrytion.Decrypt(DecodeURL(cipherText), _passEncrypt).Trim();
                return cipherText;
            }
            catch
            {
                return null;
            }
        }

        public static int DescryptInt(string cipherText)
        {
            try
            {
                int cpText = Convert.ToInt32(encrytion.Decrypt(DecodeURL(cipherText)).Trim());
                return cpText;
            }
            catch
            {
                return -1;
            }
        }

        public static string EncryptToStr(this string plainText)
        {
            return EncryptStr(plainText);
        }

        public static string EncryptToInt(this int plainText)
        {
            return EncryptInt(plainText);
        }

        public static string DecryptToStr(this string cipherText)
        {
            return DecryptStr(cipherText);
        }

        public static int DescryptToInt(this string cipherText)
        {
            return DescryptInt(cipherText);
        }

        #endregion Public Method

        #region MD5

        public static string MD5(string value)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString().ToLower();
        }

        public static string MD5(byte[] input)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(input);

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString().ToLower();
        }

        public static string MD5(Stream input)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (input != null)
                {
                    input.CopyTo(stream);
                    return MD5(stream.ToArray());
                }
            }

            return string.Empty;
        }

        public static string GetUnique(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            byte[] data = new byte[1];

            System.Security.Cryptography.RNGCryptoServiceProvider crypto = new System.Security.Cryptography.RNGCryptoServiceProvider();

            crypto.GetNonZeroBytes(data);

            data = new byte[size];

            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion MD5

        #region Private Method

        private static string EncodeURL(string strPlain)
        {
            strPlain = strPlain.Replace('+', '-').Replace('/', '_').Replace("=", "");
            return strPlain;
        }

        private static string DecodeURL(string strCipher)
        {
            if (strCipher.Length % 4 != 0)
            {
                strCipher += new string('=', 4 - (strCipher.Length % 4));
            }
            strCipher = strCipher.Replace('-', '+').Replace('_', '/');
            return strCipher;
        }

        #endregion Private Method
    }

    public class RSAEncryption
    {
        #region Private/ public key

        private static string _publicKey = @"<RSAKeyValue><Modulus>y+UPjSBCkKMQMJ691hjAPSYmorxcp/1SaNRlMz4R5y9CsS1YYdJKx4e9LrscS8AEaH7t5Pt1zn1qzBCfZDN1BZGxOxmctItd7VQV9xONL0H5EJ3m1fCUMkRGJJ4eo/PwsZN854A7zbkQ2LaBvgYg4bGlIU/wkR2+QZq/5qdnOHGeE07e0KoZfYSiGYETgsUazL3+C47V6NTg7Yfz3xhyoKDx5afBIy66KarsL7VyzHxuqH6J7jAQBu+HFlSO7uGHw92/A2h9Lx5n/oxTPES4kWm0Xaal0NZW5ZrJGdZVADiYgJQ/+4IlvUELkNS3D4PJ2zDxFIDuoOtCqjX1rhv0slyVq4jAEoiWVvnVaoBKLbakRW1S3NVfMvMHMtUGYubnuL6bhSbHeaLncxne88bMoUe3VYuGaPunnRv/p9zrJa6RaZYADxqFkMxI8HDPq6rqO/aWbKLUwnYDOSqM8ng3ZwOORJow4Q8L2tJI1eCFoe5c1CAcs/3q+vOhliuxPtg1s3B0cDEFIhh3uvSM62yYVRG52MsQes9cEGx/SJ7NCNrro/ejqPVA2NgdN5vukEcx5i5NNEnhuUfTGk+PYj2IfYA0jjvH3Tj9PToJDZFUE2jIxz8tGjoPBiMoLIlOG4ZFntLkNZGlXqGkmlksL1V9CngDq14Mago7cXTQGl4UAE0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string _privateKey = @"<RSAKeyValue><Modulus>y+UPjSBCkKMQMJ691hjAPSYmorxcp/1SaNRlMz4R5y9CsS1YYdJKx4e9LrscS8AEaH7t5Pt1zn1qzBCfZDN1BZGxOxmctItd7VQV9xONL0H5EJ3m1fCUMkRGJJ4eo/PwsZN854A7zbkQ2LaBvgYg4bGlIU/wkR2+QZq/5qdnOHGeE07e0KoZfYSiGYETgsUazL3+C47V6NTg7Yfz3xhyoKDx5afBIy66KarsL7VyzHxuqH6J7jAQBu+HFlSO7uGHw92/A2h9Lx5n/oxTPES4kWm0Xaal0NZW5ZrJGdZVADiYgJQ/+4IlvUELkNS3D4PJ2zDxFIDuoOtCqjX1rhv0slyVq4jAEoiWVvnVaoBKLbakRW1S3NVfMvMHMtUGYubnuL6bhSbHeaLncxne88bMoUe3VYuGaPunnRv/p9zrJa6RaZYADxqFkMxI8HDPq6rqO/aWbKLUwnYDOSqM8ng3ZwOORJow4Q8L2tJI1eCFoe5c1CAcs/3q+vOhliuxPtg1s3B0cDEFIhh3uvSM62yYVRG52MsQes9cEGx/SJ7NCNrro/ejqPVA2NgdN5vukEcx5i5NNEnhuUfTGk+PYj2IfYA0jjvH3Tj9PToJDZFUE2jIxz8tGjoPBiMoLIlOG4ZFntLkNZGlXqGkmlksL1V9CngDq14Mago7cXTQGl4UAE0=</Modulus><Exponent>AQAB</Exponent><P>7o9wZGXLl31dikwwG+CZBlsDOVV9qKAxtwkxBrSZbG0oDUZB32wpPgTXDj8QS71GzBk92KPIUv5Z4Pt29GKp20s72ToFiFelhoqEuXUZ70Qu+3FFU5EGGM9w4vW1WXscDwt+KrCLns2yjEkirG+1ir7NI+hDPZ2ejOeMS8yMUB7oFqTo1y4shzScyntfZKAJHQbp00l51AlFnpYH1vdlpjaBmABXR48NKNQlEuL83tpqCVoGDny0B2tDFuwsaZffnpg8FSSk9z8b3SGW/tSpIMN/WmDZy+ZCf+mq/1KnEmRC+Cex+Qgc8zztmKQMGz2mzI2QCU1hkedzYXDsHx4iyw==</P><Q>2szethWZ0hyJqX2JI4TZ1VVQBNOsooaUdIbOeTSemDn1Nxj2+jW4KleS1yrGTS4WJZQL2R2iKjYJQZmPMyHYJVYRYNjY1iR/iYhsDGlqicJfIWBBYzCJrasD3bKwDckqEVPu5FlnxwBcH1qUqAtAvWYMP/e7jvmVscNzAJMVRGXxT5EfrrgJToZYsKI1z4tm2u2hgZe2KTNpOKnJYlsIdNsOeIHB+JcHWg8hwOeFrQDF3OjZWjh5GacrCf0N/MZu3oRgLL1UdMjvJIXJ0HxpBbXQv/wtw2Ct8TIFw9YtrOeEAISeJVgdobS3JXI9I3Tcs5dPWjd30ZPybN9yOgvORw==</Q><DP>vG3V86coEYc3PEaDdXGIzSlUOZQaRfgKbK246Lf7u1XY3etRmpz/UnpWN21+fKSLaCjD3fs3/r3i1j8prUzFJ26cXi9jMVcxajy9KxOgoYm0pmzIuoIBU9V+L/auAHiqKAQG6sc58Pw+fGpEFfnCZk3hEOyjN+bo1hkKitCNP7e50DD/rP9OspjPA2xBY+S4bhII3RfbP6z3LZUJqjUNOCUzZZFXXzFRzp8KjYl2/Lt+J9bQk97SAE4r1s5DIkCQo99RDc488wIJCzg94RiuYi+oxwVa4qAEhK5Xn4Z0aqWA9Cu+epp663GDph5lZZrjxgxG6eISIEG1tBCSoN2X0w==</DP><DQ>h+MA1lkoBSQQIyGCEHsrxqCLZ9QGvZGTlaM7jG8vqB8muidZDCBc1n85BUTdj4V137TIfvk2g7y3lvRFyV3VnaqMdHd4g9Z7FvGRGUxHDHOZHWNoeK/mceNqLUASsx0icUyNa2hTWXZRwG9DL/J9LKO6K+gxpiQ/4f0e6VZvuOEPM3lQEfTeYtRxNJIplVJgeMtoqNGOIpx5VuWr2tfqsphxrkn4K+mweTA4qFOh48Y9HGvIwY2dOF9oDXYKP9kFdlEEYgTb9QPt8eLdagw6NX+ru/Bkg5kBrnCAwBW+nEnU43V5unu1O36+2BTQ/7pOb4Mjl0YnthOKVi+YA5DddQ==</DQ><InverseQ>mxIjkSoIjvHHlALhi7jnwdXKGwBXywmZFpZYX3FIukeo3Vp1WmQKSIqU3xWQseLZgGZk1M3b+wb3wLD74RsTiMD/8kVf2Gp2t9B/KApueCOUYqUivj7we1e8+3LQb/r8mt/S7FWlVbzL5ojNGQ1cK0RaXGNr3XVhMLz3whJZbsGSnl60T65+jng4RTy6G5UVHkV7W/LG9IROL72adYausS9AhkxOZPmzzACLzCcq8xgM54SwYkyp3A47TX5zrmM0l2N5gNERA1H8vvOP820qP4dah3jizoWu0UJgBIPQDkh2TyhGvBWSebClobpS531eKFEqWxENXBcKTw+tDSROQw==</InverseQ><D>Hw76anXyjdO5AJ79reJBTk9EbV+iAZjt97f63m5jDcM+Nn0AzI3bBD/iCn9IFSXfxgcnt8yyKk0ieolBrmLEPZ/uzmDdZQchvKglXGinBf7XsLdM1WP7Pxj1UliftDvRe0tLKHx5H6JENa6/XrHSSm7kB4oEzCYquBWZi8oOSCPOf4RxHnn6vgr5IkduMGpT5cI/M4WDgh8DUEt4U5CGzX8WufPu0KwqgXRe2/mYzhtaQ+JaomTB3DyaaEVjLHJAWw6pLRy6jPXlw3meqDyTnNrsNaPP+FreC8WjQoNKmWkFmfh7MpWlqAmDeIFejUd29SOkZYoqT2rm0cS8ctQhVtBmWXUWkwR4UZJqb4rCDNCTfl2+zUqOw8syligt9D1YuBZLnxfVVTX9vd42xMYbXWx5xUpb3qRmavtaSd23D9HMFJXQMK9EBBw8YYCpkV3hH7dfbeKjHgJtI/Y5cV+WmXjhxsWUnXk9R57PYjLU4UbFmjUYTkWATHF+aocECJ+qDdynmuUPJdAYr0ALPKgFtnRRgLYzCAOv8g5pB4f2wCHfV07K5vggZq3Q0KO8EjdqQwQARuOyBHtWuCO2/bSh4V1oRVzarvOmmgrKniVDjeqK7kw2QdGv/tYLXsNVJE3k+wNrplEDxDUHAbCpCk6IdTzGvG2iPt+Xu26WYI6jFMU=</D></RSAKeyValue>";

        #endregion Private/ public key

        public static string GenerateNewKeys(int? bits)
        {
            if (bits == null || bits == 0)
            {
                bits = 4096;
            }
            CspParameters cspParams = new CspParameters(1);
            var rsaCrypto = new RSACryptoServiceProvider(bits ?? 4096, cspParams);

            return $"Private Key: {rsaCrypto.ToXmlString(true)}|Public Key: {rsaCrypto.ToXmlString(false)}";
        }

        public static string EncryptWithPublic(string pCleartext, string pPublicKey = null)
        {
            if (string.IsNullOrEmpty(pPublicKey))
            {
                pPublicKey = _publicKey;
            }
            RSACryptoServiceProvider rsaCryp = new RSACryptoServiceProvider();
            rsaCryp.FromXmlString(pPublicKey);

            byte[] plainbytes = Encoding.Unicode.GetBytes(pCleartext);
            byte[] cipherbytes = rsaCryp.Encrypt(plainbytes, false);

            return Convert.ToBase64String(cipherbytes);
        }

        public static string DecryptWithPrivate(string pCiphertext, string pPrivateKey = null)
        {
            if (string.IsNullOrEmpty(pPrivateKey))
            {
                pPrivateKey = _privateKey;
            }
            string cleartext = "";
            RSACryptoServiceProvider rsaCryp = new RSACryptoServiceProvider();
            rsaCryp.FromXmlString(pPrivateKey);
            try
            {
                byte[] cipherbytes = Convert.FromBase64String(pCiphertext);
                byte[] plain = rsaCryp.Decrypt(cipherbytes, false);
                cleartext = System.Text.Encoding.Unicode.GetString(plain);
            }
            catch
            {
                throw;
            }
            return cleartext;
        }
    }
}
