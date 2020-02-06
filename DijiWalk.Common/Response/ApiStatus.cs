using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{
    public static class ApiStatus
    {

        /// <summary>
        /// Correspond à un status 200, tout est bon
        /// </summary>
        public static int Ok
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// L'objet n'est pas instancié dans la référence d'un object
        /// </summary>
        public static int NotSetInstance
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Clé étrangère conflict
        /// </summary>
        public static int Conflict
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Valeur null
        /// </summary>
        public static int NullValue
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Informations invalide lors de l'authentification (player et organizer)
        /// </summary>
        public static int InvalidAuth
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Impossible de delete, dépend d'un élément (manuel)
        /// </summary>
        public static int CantDelete
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// Erreur inconnu (pas encore)
        /// </summary>
        public static int UnknownError
        {
            get
            {
                return 99;
            }
        }
    }
}
