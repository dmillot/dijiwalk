//-----------------------------------------------------------------------
// <copyright file="IApiResponse.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Common.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface for the ApiResponse class
    /// </summary>
    public interface IApiResponse
    {

        /// <summary>
        /// Récupère le message par défault quand le delete réussi.
        /// </summary>
        /// <returns>Message</returns>
        string GetMessageDelete();

        /// <summary>
        /// Récupère le message par défault quand l'ajout réussi.
        /// </summary>
        /// <returns>Message</returns>
        string GetMessageAdd();

        /// <summary>
        /// Récupère le message par défault quand l'update réussi.
        /// </summary>
        /// <returns>Message</returns>
        string GetMessageUpdate();


        /// <summary>
        /// Permet de traduire l'erreur en message d'erreur
        /// </summary>
        /// <param name="error">Message de l'erreur</param>
        /// <returns>Traduction de l'erreur</returns>
        string TranslateError(Exception error);



    }
}
