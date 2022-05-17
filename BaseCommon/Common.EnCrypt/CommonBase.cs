using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseCommon.Common.EnCrypt
{
    public class CommonBase
    {
        #region MD5
        public static string ToMD5(string value)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));

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

        public static bool IsStrongPassword(string password, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty(password))
            {
                errorMessage = "Mật khẩu không được rỗng";
                return false;
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,50}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ cái thường.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa.";
                return false;
            }
            if (!hasMiniMaxChars.IsMatch(password))
            {
                errorMessage = "Mật khẩu không được nhỏ hơn 8 ký tự."; //8 -> 50 ký tự
                return false;
            }
            else if (!hasUpperChar.IsMatch(password) && !hasLowerChar.IsMatch(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ cái.";
            }
            else if (!hasNumber.IsMatch(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một giá trị số.";
                return false;
            }
            else if (!hasSymbols.IsMatch(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt.";
                return false;
            }

            return true;
        }

        #endregion
        #region Endcode

        public static string ToBase64Encode(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
        public static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion
    }
}
