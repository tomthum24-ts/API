using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
