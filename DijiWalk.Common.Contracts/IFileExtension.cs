//-----------------------------------------------------------------------
// <copyright file="FileExtension.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface for the FileExtension class
    /// </summary>
    public interface IFileExtension
    {
        /// <summary>
        /// Method to get extension of file
        /// </summary>
        /// <param name="base64">File base64</param>
        /// <returns>Extension of file</returns>
        string GetExtension(string base64);

        /// <summary>
        /// Method to get name of file
        /// </summary>
        /// <param name="pictureUrl">URL of the picture</param>
        /// <returns>Name of file</returns>
        string GetName(string pictureUrl);



    }
}
