//-----------------------------------------------------------------------
// <copyright file="TeamPlayer.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    /// <summary>
    /// Class definissant le Joueur d'une Team
    /// </summary>
    public partial class TeamPlayer
    {
        /// <summary>
        /// Obtient ou définit l'Id de la Team
        /// </summary>
        public int IdTeam { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Joueur de la Team
        /// </summary>
        public int IdPlayer { get; set; }

        /// <summary>
        /// Obtient ou définit le Joueur de la Team
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Obtient ou définit la Team du Joueur
        /// </summary>
        public virtual Team Team { get; set; }
    }
}
