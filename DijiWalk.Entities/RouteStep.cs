//-----------------------------------------------------------------------
// <copyright file="RouteStep.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant l'Etape d'une Route
    /// </summary>
    public partial class RouteStep
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance de l'Etape d'une Route
        /// </summary>
        public RouteStep()
        {
            TeamRoutes = new HashSet<TeamRoute>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de la Route
        /// </summary>
        public int IdRoute { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Etape de la Route
        /// </summary>
        public int IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit l'Ordre de l'Etape de la Route
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Obtient ou définit la Route rataché à de l'Etape de la Route
        /// </summary>
        public virtual Route Route { get; set; }

        /// <summary>
        /// Obtient ou définit l'Etape de la Route
        /// </summary>
        public virtual Step Step { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }
    }
}
