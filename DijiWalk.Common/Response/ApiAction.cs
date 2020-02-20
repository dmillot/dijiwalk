using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{

    public static class ApiAction
    {

        /// <summary>
        /// Action add avec le message de validation de création
        /// </summary>
        public static string Add
        {
            get
            {
                return "Création effectuée avec succés !";
            }
        }

        /// <summary>
        /// Action update avec le message de validation d'édition
        /// </summary>
        public static string Update
        {
            get
            {
                return "Édition effectuée avec succés !";
            }
        }

        /// <summary>
        /// Action delete avec le message de validation de suppression
        /// </summary>
        public static string Delete
        {
            get
            {
                return "Suppression effectuée avec succés !";
            }
        }

        /// <summary>
        /// Action delete avec le message de validation de suppression
        /// </summary>
        public static string Validate
        {
            get
            {
                return "Votre image est validée !";
            }
        }

        /// <summary>
        /// Action delete avec le message de validation de suppression
        /// </summary>
        public static string WaitingValidation
        {
            get
            {
                return "Votre image est en attente de la validation de l'organisateur !";
            }
        }

        /// <summary>
        /// Action delete avec le message de validation de suppression
        /// </summary>
        public static string NotValidation
        {
            get
            {
                return "Votre image n'a pas été validée par l'organisateur !";
            }
        }
    }
}
