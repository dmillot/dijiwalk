﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{

    public enum ApiStatus
    {
        /// <summary>
        /// Correspond à un status 200, tout est bon
        /// </summary>
        Ok = 1,
        /// <summary>
        /// L'objet n'est pas instancié dans la référence d'un object
        /// </summary>
        NotSetInstance = 2,
        /// <summary>
        /// Clé étrangère conflict
        /// </summary>
        Conflict = 3,
        /// <summary>
        /// Valeur null
        /// </summary>
        NullValue = 4,
        /// <summary>
        /// Informations invalide lors de l'authentification (player et organizer)
        /// </summary>
        InvalidAuth = 5,
        /// <summary>
        /// Impossible de delete, dépend d'un élément (manuel)
        /// </summary>
        CantDelete = 6,
        /// <summary>
        /// Erreur inconnu (pas encore)
        /// </summary>
        UnknownError = 99
    };

}