using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Utilities
{
    public class StringHelpers
    {
        public static string Normalization(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.Normalize(NormalizationForm.FormKC);
            return text;
        }
    }
}
