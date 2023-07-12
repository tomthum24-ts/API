using JetBrains.Annotations;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseCommon.Model
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
        public static string Left(this string str, int len)
        {
            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            return str.RemovePostFix(StringComparison.Ordinal, postFixes);
        }

        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="comparisonType">String comparison type</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix, comparisonType))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds a "..." postfix to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(str) || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        [ContractAnnotation("null => true")]
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FirstCharToLowerCase([CanBeNull] this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string FirstCharToUpperCase([CanBeNull] this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsUpper(str[0]))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ToURL(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = new Regex(@"[,.\[\]]").Replace(url, "");
                StringBuilder builder = new StringBuilder();
                string str = "ă\x00e2đ\x00ea\x00f4ơư\x00e0ả\x00e3ạ\x00e1ằẳẵặắầẩẫậấ\x00e8ẻẽẹ\x00e9ềểễệế\x00ecỉĩị\x00ed\x00f2ỏ\x00f5ọ\x00f3ồổỗộốờởỡợớ\x00f9ủũụ\x00faừửữựứỳỷỹỵ\x00fdĂ\x00c2Đ\x00ca\x00d4ƠƯ\x00c0Ả\x00c3Ạ\x00c1ẰẲẴẶẮẦẨẪẬẤ\x00c8ẺẼẸ\x00c9ỀỂỄỆẾ\x00ccỈĨỊ\x00cd\x00d2Ỏ\x00d5Ọ\x00d3ỒỔỖỘỐỜỞỠỢỚ\x00d9ỦŨỤ\x00daỪỬỮỰỨỲỶỸỴ\x00dd\u0111\u0110";
                string str2 = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYdd";
                string str3 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789";
                for (int i = 0; i < url.Length; i++)
                {
                    int index = str.IndexOf(url[i]);
                    if (index > -1)
                    {
                        builder.Append(str2[index]);
                    }
                    else if (str3.IndexOf(url[i]) > -1)
                    {
                        builder.Append(url[i]);
                    }
                    else
                    {
                        builder.Append('-');
                    }
                }
                return Regex.Replace(Regex.Replace(builder.ToString(), "^(-)*|(-)*$", ""), "-(-)*", "-").ToLower();
            }
            else
            {
                return "";
            }
        }

        public static string ToAlias(this string url)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = url.Normalize(NormalizationForm.FormD);
            url = regex.Replace(temp, String.Empty).Replace('\u0111', 'd')
                .Replace('\u0110', 'D').Replace(",", "").Replace("“", "").Replace("”", "")
                .Replace("?", "").Replace("'", "").Replace(")", "").Replace("(", "").Replace("&", "-");
            url = StripTagsCharArray(url);
            url = ToURL(url);
            url = Regex.Replace(Regex.Replace(url, "^(-)*|(-)*$", ""), "-(-)*", "-").ToLower();
            return url;
        }

        public static string TrimForSearch(this string url)
        {
            url = StripTagsCharArray(url);
            url = ToURL(url);
            url = Regex.Replace(Regex.Replace(url, "^(-)*|(-)*$", ""), "-(-)*", "").ToLower();
            return url;
        }

        public static string StripTagsCharArray(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }
            return source;
        }

        public static string Replace(this string input, Dictionary<string, string> values)
        {
            if (values != null && values.Any())
            {
                foreach (var value in values)
                {
                    input = input.Replace(value.Key, value.Value);
                }
            }

            return input;
        }
    }
}
