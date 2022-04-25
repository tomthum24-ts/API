using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace API.Common
{
    public static class SecurityHelper
    {
        #region Encryption and decryption

        // salt size must be at least 8 bytes, we will use 16 bytes
        private static readonly byte[] Salt = Encoding.Unicode.GetBytes("7BANANAS");

        // iterations must be at least 1000, we will use 2000.
        private static readonly int iterations = 2000;

        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;

            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            var aes = Aes.Create(); // abstract class factory method

            var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, iterations);

            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key

            aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }

                encryptedBytes = ms.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;

            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);

            var aes = Aes.Create();

            var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, iterations);

            aes.Key = pbkdf2.GetBytes(32);

            aes.IV = pbkdf2.GetBytes(16);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }

                plainBytes = ms.ToArray();
            }

            return Encoding.Unicode.GetString(plainBytes);
        }

        public static CryptoStream EncryptStream(Stream stm, string password)
        {
            var aes = Aes.Create(); // abstract class factory method

            var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, iterations);

            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key

            aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV

            ToBase64Transform base64Transform = new ToBase64Transform();

            CryptoStream base64EncodedStream = new CryptoStream(stm, base64Transform, CryptoStreamMode.Write);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            CryptoStream cryptoStream = new CryptoStream(base64EncodedStream, encryptor, CryptoStreamMode.Write);

            return cryptoStream;
        }

        public static Stream DecryptStream(Stream stm, string password)
        {
            var aes = Aes.Create(); // abstract class factory method

            var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, iterations);

            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key

            aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV

            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);

            CryptoStream base64DecodedStream = new CryptoStream(stm, base64Transform, CryptoStreamMode.Read);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);

            return decryptedStream;
        }

        #endregion Encryption and decryption

        #region Hashes

        public static string GenerateSalt(int byteLength = 32)
        {
            // 32 Bytes will give 256 bits.
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[byteLength];

                randomNumberGenerator.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public static string ReGenerateSaltAndHashedPassword(string password, string salt)
        {
            var sha = SHA256.Create();

            var saltedPassword = password + salt;

            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }

        public static string EncryptSha256(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }

        public static string EncryptSha512(string value)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));

                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }

        public static string Md5Hash(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(value));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        #endregion Hashes

        #region Signatures

        public static string PublicKey;

        public static string ToXmlStringExt(this RSA rsa, bool includePrivateParameters)
        {
            var p = rsa.ExportParameters(includePrivateParameters);

            XElement xml;

            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                    new XElement("Exponent", Convert.ToBase64String(p.Exponent)),
                    new XElement("P", Convert.ToBase64String(p.P)),
                    new XElement("Q", Convert.ToBase64String(p.Q)),
                    new XElement("DP", Convert.ToBase64String(p.DP)),
                    new XElement("DQ", Convert.ToBase64String(p.DQ)),
                    new XElement("InverseQ", Convert.ToBase64String(p.InverseQ))
                );
            }
            else
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                    new XElement("Exponent", Convert.ToBase64String(p.Exponent)));
            }

            return xml.ToString();
        }

        public static void FromXmlStringExt(this RSA rsa, string parametersAsXml)
        {
            var xml = XDocument.Parse(parametersAsXml);

            var root = xml.Element("RSAKeyValue");

            var p = new RSAParameters
            {
                Modulus = Convert.FromBase64String(root.Element("Modulus").Value),
                Exponent = Convert.FromBase64String(root.Element("Exponent").Value)
            };

            if (root.Element("P") != null)
            {
                p.P = Convert.FromBase64String(root.Element("P").Value);
                p.Q = Convert.FromBase64String(root.Element("Q").Value);
                p.DP = Convert.FromBase64String(root.Element("DP").Value);
                p.DQ = Convert.FromBase64String(root.Element("DQ").Value);
                p.InverseQ = Convert.FromBase64String(root.Element("InverseQ").Value);
            }

            rsa.ImportParameters(p);
        }

        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);

            var sha = SHA256.Create();

            var hashedData = sha.ComputeHash(dataBytes);

            var rsa = RSA.Create();

            PublicKey = rsa.ToXmlStringExt(false); // exclude private key

            return Convert.ToBase64String(rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }

        public static bool ValidatesSignature(string data, string signature)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);

            var sha = SHA256.Create();

            var hashedData = sha.ComputeHash(dataBytes);

            byte[] signatureBytes = Convert.FromBase64String(signature);

            var rsa = RSA.Create();

            rsa.FromXmlStringExt(PublicKey);

            return rsa.VerifyHash(hashedData, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        #endregion Signatures

        #region SQL Ininection

        public static bool CheckForSQLInjection(string valueInput)
        {
            bool isSQLInjection = false;

            if (string.IsNullOrWhiteSpace(valueInput)) return isSQLInjection;

            string[] sqlCheckList = { "--",";--",";","/*","*/","@@","@","char","nchar","varchar","nvarchar","alter","begin",
                                    "cast","create","cursor","declare","delete","drop","end","exec","execute","fetch",
                                    "insert","kill","select","sys","sysobjects","syscolumns","table","update"};

            string CheckString = valueInput.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    isSQLInjection = true;
                    break;
                }
            }

            return isSQLInjection;
        }

        #endregion SQL Ininection

        public static bool VerifyRS256SignatureOfJWT(string tokenStr)
        {
            string[] tokenParts = tokenStr.Split('.');

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.ImportParameters(new RSAParameters()
            {
                Modulus = FromBase64Url("sNtWb4JVSGI3DFUsV-OUN-88Rn72TJSHcJ9xHfO6Ar7_GMUrudlHRkte55tTen36h0VTwuiaiitaiNAfYPEhlXMmHIiss-njhBjxSocfLMJrboS6h3rcMpKVvMM2PSHCkQKWAG0fyKFieir51r6YO9SByU7miMw66RJr-4E6bkeKz4d3fnM86LCMfLEi54rpf3be2PnBF1sSqSxuTAbF2wTQEhMimK8OhCiFuQhKwxOAqMBtQ55YRV3vGGyBKCzbPY3WEeZEvhdqf1XS_7u1DZ34oUi2haI-XkUQVplfkgZJpcgngt_6VxDLLCsy8jDESDhvCXGdsBDTqS6EyGb8_w"),
                Exponent = FromBase64Url("AQAB")
            });

            SHA256 sha256 = SHA256.Create();

            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);

            rsaDeformatter.SetHashAlgorithm("SHA256");

            if (rsaDeformatter.VerifySignature(hash, FromBase64Url(tokenParts[2])))
            {
                return true;
            }

            return false;
        }

        public static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
    }
}
