//-----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface for the StringExtension class
    /// </summary>
    public interface IStringExtension
    {
        /// <summary>
        /// Remove all accent of string variable
        /// </summary>
        /// <param name="text">Variable with accent and whitespace</param>
        /// <returns>Text without accent and whitespace</returns>
        string RemoveDiacriticsAndWhiteSpace(string text);

    }
}
