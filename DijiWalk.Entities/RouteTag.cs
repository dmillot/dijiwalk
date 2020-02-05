//-----------------------------------------------------------------------
// <copyright file="RouteTag.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    /// <summary>
    /// Class definissant le Tag d'une Route
    /// </summary>
    public partial class RouteTag
    {
        /// <summary>
        /// Obtient ou définit l'Id de la Route
        /// </summary>
        public int IdRoute { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Tag
        /// </summary>
        public int IdTag { get; set; }

        /// <summary>
        /// Obtient ou définit la Route
        /// </summary>
        public virtual Route Route { get; set; }

        /// <summary>
        /// Obtient ou définit le Tag rataché à la Route
        /// </summary>
        public virtual Tag Tag { get; set; }
    }
}
