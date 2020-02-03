//-----------------------------------------------------------------------
// <copyright file="TeamRoute.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;

    /// <summary>
    /// Class definissant l'Ordre des Etapes d'une Team
    /// </summary>
    public partial class TeamRoute
    {
        /// <summary>
        /// Obtient ou définit l'Id de la Route de la Team
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Jeu correspondant à la Route de la Team
        /// </summary>
        public int IdGame { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Team
        /// </summary>
        public int IdTeam { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Route
        /// </summary>
        public int? IdRoute { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Etape de la Route de la Team
        /// </summary>
        public int? IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit l'Ordre de l'Etape de la Route de la Team
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de la Validation de l'Etape de la Route de la Team
        /// </summary>
        public DateTime? ValidationDate { get; set; }

        /// <summary>
        /// Obtient ou définit l'Etape de la Route rataché à la Route de la Team
        /// </summary>
        public virtual RouteStep RouteStep { get; set; }

        /// <summary>
        /// Obtient ou définit le Jeu rataché à la Route de la Team
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Obtient ou définit la Team
        /// </summary>
        public virtual Team Team { get; set; }
    }
}
