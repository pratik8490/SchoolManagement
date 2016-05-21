using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Helper
{
    public static class StringExtension
    {
        public static string ToSubstring(this string text, int index)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > index)
                text = text.Substring(0, index) + "...";

            return text;
        }
    }
}
