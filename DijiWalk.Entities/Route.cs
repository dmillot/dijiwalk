//-----------------------------------------------------------------------
// <copyright file="Route.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant une Route
    /// </summary>
    public partial class Route
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Route
        /// </summary>
        public Route()
        {
            Games = new HashSet<Game>();
            RouteSteps = new HashSet<RouteStep>();
            RouteTags = new HashSet<RouteTag>();
        }

        public Route(Route r)
        {
            Id = r.Id;
            Name = r.Name;
            Description = r.Description;
            Handicap = r.Handicap;
            Time = r.Time;
            Distance = r.Distance;
            IdOrganizer = r.IdOrganizer;
            Organizer = r.Organizer;
            Games = new HashSet<Game>();
            RouteSteps = new HashSet<RouteStep>();
            RouteTags = new HashSet<RouteTag>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de la Route
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom de la Route
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtient ou définit la Description de la Route
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit l'Handicap de la Route
        /// </summary>
        public bool? Handicap { get; set; }

        /// <summary>
        /// Obtient ou définit le Temps moyen de la Route
        /// </summary>
        public TimeSpan? Time { get; set; }

        /// <summary>
        /// Obtient ou définit la Distance de la Route
        /// </summary>
        public int? Distance { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de L'Organisateur de la Route
        /// </summary>
        public int? IdOrganizer { get; set; }

        /// <summary>
        /// Obtient ou définit l'Organisateur ayant créé la Route
        /// </summary>
        public virtual Organizer Organizer { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Jeux ayant utilisé la Route
        /// </summary>
        public virtual ICollection<Game> Games { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Etapes de la Route
        /// </summary>
        public virtual ICollection<RouteStep> RouteSteps { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Tags de la Route
        /// </summary>
        public virtual ICollection<RouteTag> RouteTags { get; set; }
    }
}
