//-----------------------------------------------------------------------
// <copyright file="ICryption.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface for the Cryption class
    /// </summary>
    public interface ICryption
    {
        /// <summary>
        /// Permet d'encrypter le password rentrer par le participant / organisateur
        /// </summary>
        /// <param name="password">Password de l'utilisateur</param>
        /// <returns>Retourne le password encrypté</returns>
        string Encrypt(string password);

        /// <summary>
        /// Permet de décrypter le password passer en paramètre
        /// </summary>
        /// <param name="password">Password crypté</param>
        /// <returns>Retourne le password décrypté</returns>
        string Decrypt(string decryptText);



    }
}
