using System.Text.RegularExpressions;

namespace HQPlus.WebExtraction.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNewLineCharacter(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return Regex.Replace(text, @"(\n)+", string.Empty);
        }

        public static string GetStarCount(this string text)
        {
            return Regex.Match(text, @"\d+").Value;
        }
    }
}
