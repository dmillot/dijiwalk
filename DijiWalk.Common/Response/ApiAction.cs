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
    }
}
