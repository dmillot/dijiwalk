using DijiWalk.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{
    public class ApiResponse : IApiResponse
    {

        private string _messageDelete = "Suppression effectuée avec succés !";
        public string MessageDelete { get { return _messageDelete; } set { _messageDelete = value; } }

        private string _messageAdd = "Ajout effectué avec succés !";
        public string MessageAdd { get { return _messageAdd; } set { _messageAdd = value; } }

        private string _messageUpdate = "Mise à jour effectuée avec succés !";
        public string MessageUpdate { get { return _messageUpdate; } set { _messageUpdate = value; } }

        /// <summary>
        /// Permet de traduire l'erreur en message d'erreur
        /// </summary>
        /// <param name="error">Message de l'erreur</param>
        /// <returns>Traduction de l'erreur</returns>
        public string TranslateError(Exception error)
        {
            switch (error.InnerException != null ? error.InnerException.Message : error.Message)
            {
                case "Object reference not set to an instance of an object.":
                    return "L'élément est introuvable !";
                case string a when a.Contains("est en conflit avec la contrainte"):
                    return "Impossible de supprimer cette élément, il dépend d'un autre !";
                default:
                    return "Erreur inconnu";
            }
        }


        /// <summary>
        /// Récupère le message par défault quand le delete réussi.
        /// </summary>
        /// <returns>Message</returns>
        public string GetMessageDelete()
        {
            return MessageDelete;
        }


        /// <summary>
        /// Récupère le message par défault quand l'ajout réussi.
        /// </summary>
        /// <returns>Message</returns>
        public string GetMessageAdd()
        {
            return MessageAdd;
        }

  

        /// <summary>
        /// Récupère le message par défault quand l'update réussi.
        /// </summary>
        /// <returns>Message</returns>
        public string GetMessageUpdate()
        {
            return MessageUpdate;
        }

    }

}

