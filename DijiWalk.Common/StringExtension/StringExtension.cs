using DijiWalk.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DijiWalk.Common.StringExtension
{
    public class StringExtension : IStringExtension
    {

        /// <summary>
        /// Remove all accent of string variable
        /// </summary>
        /// <param name="text">Variable with accent and whitespace</param>
        /// <returns>Text without accent and whitespace</returns>
        public string RemoveDiacriticsAndWhiteSpace(string text)
        {

            var withoutWhiteSpace = Regex.Replace(text, @"\s+", "");
            var normalizedString = withoutWhiteSpace.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
