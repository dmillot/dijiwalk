//-----------------------------------------------------------------------
// <copyright file="Tag.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant un Tag
    /// </summary>
    public partial class Tag
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'un Tag
        /// </summary>
        public Tag()
        {
            RouteTags = new HashSet<RouteTag>();
            StepTags = new HashSet<StepTag>();
        }

        /// <summary>
        /// Obtient ou définit l'Id du Tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit la Description du Tag
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit la Couleur du Tag
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom du Tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<RouteTag> RouteTags { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<StepTag> StepTags { get; set; }
    }
}
