using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseCommon.Utilities
{
    public static class StringHelpers
    {
        public static string Normalization(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.Normalize(NormalizationForm.FormKC);
            return text;
        }
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            return str.RemovePostFix(StringComparison.Ordinal, postFixes);
        }
        public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (postFixes == null || postFixes.Length <= 0)
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
        public static string Left(this string str, int len)
        {
            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }
            return str.Substring(0, len);
        }
        private static readonly string[] VietnameseSigns = {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string GenerateIdFromString(this string phrase)
        {
            string str = phrase.RemoveSign4VietnameseString();
            str = str.RemoveSpecialCharacters();
            str = Regex.Replace(str, @"\s+", " ").Trim();     // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s", "_"); // hyphens
            return str.ToUpper();
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        #region Random

        public static readonly char[] UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static readonly char[] LowerChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public static readonly char[] NumberChars = "0123456789".ToCharArray();
        public static readonly char[] SpecialChars = "!@#$%^*&".ToCharArray();
        public static string Generate(int length, bool isIncludeUpper = true, bool isIncludeLower = true, bool isIncludeNumber = true, bool isIncludeSpecial = false)
        {
            var chars = new List<char>();

            if (isIncludeUpper)
            {
                chars.AddRange(UpperChars);
            }

            if (isIncludeLower)
            {
                chars.AddRange(LowerChars);
            }

            if (isIncludeNumber)
            {
                chars.AddRange(NumberChars);
            }

            if (isIncludeSpecial)
            {
                chars.AddRange(SpecialChars);
            }

            return GenerateRandom(length, chars.ToArray());
        }
        public static string GenerateRandom(int length, params char[] chars)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), $"{length} cannot be less than zero.");
            }

            if (chars?.Any() != true)
            {
                throw new ArgumentOutOfRangeException(nameof(chars), $"{nameof(chars)} cannot be empty.");
            }

            chars = chars.Distinct().ToArray();

            const int maxLength = 256;

            if (maxLength < chars.Length)
            {
                throw new ArgumentException($"{nameof(chars)} may contain more than {maxLength} chars.", nameof(chars));
            }

            var outOfRangeStart = maxLength - (maxLength % chars.Length);

            using (var rng = RandomNumberGenerator.Create())
            {
                var sb = new StringBuilder();

                var buffer = new byte[128];

                while (sb.Length < length)
                {
                    rng.GetBytes(buffer);

                    for (var i = 0; i < buffer.Length && sb.Length < length; ++i)
                    {
                        if (outOfRangeStart <= buffer[i])
                        {
                            continue;
                        }

                        sb.Append(chars[buffer[i] % chars.Length]);
                    }
                }

                return sb.ToString();
            }
        }

        #endregion Random

    }
}
