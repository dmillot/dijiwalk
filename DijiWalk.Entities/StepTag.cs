//-----------------------------------------------------------------------
// <copyright file="StepTag.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    /// <summary>
    /// Class definissant le Tag d'une étape
    /// </summary>
    public partial class StepTag
    {
        /// <summary>
        /// Obtient ou définit l'Id de l'étape
        /// </summary>
        public int IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Tag
        /// </summary>
        public int IdTag { get; set; }

        /// <summary>
        /// Obtient ou définit l'étape
        /// </summary>
        public virtual Step Step { get; set; }

        /// <summary>
        /// Obtient ou définit le Tag rataché à la Route
        /// </summary>
        public virtual Tag Tag { get; set; }
    }
}
