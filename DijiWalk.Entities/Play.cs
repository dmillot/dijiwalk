//-----------------------------------------------------------------------
// <copyright file="Play.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant la participation d'une Equipe à un Jeu
    /// </summary>
    public partial class Play
    {
        /// <summary>
        /// Obtient ou définit l'Id du Jeu correspondant
        /// </summary>
        public int IdGame { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Equipe participant
        /// </summary>
        public int IdTeam { get; set; }

        /// <summary>
        /// Obtient ou définit le Score de l'Equipe sur le Jeu
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de Debut de l'Equipe sur le Jeu
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de Fin de l'Equipe sur le Jeu
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Obtient ou définit le Temps de l'Equipe sur le Jeu
        /// </summary>
        public TimeSpan? Time { get; set; }

        /// <summary>
        /// Obtient ou définit le Jeu correspondant
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Obtient ou définit l'Equipe participant
        /// </summary>
        public virtual Team Team { get; set; }
    }
}
