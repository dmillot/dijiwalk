//-----------------------------------------------------------------------
// <copyright file="Team.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant une Team
    /// </summary>
    public partial class Team
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Team
        /// </summary>
        public Team()
        {
            Plays = new HashSet<Play>();
            TeamAnswers = new HashSet<TeamAnswer>();
            TeamPlayers = new HashSet<TeamPlayer>();
            TeamRoutes = new HashSet<TeamRoute>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de la Team
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom de la Team
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Capitaine de la Team
        /// </summary>
        public int? IdCaptain { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Organisateur de la Team
        /// </summary>
        public int? IdOrganizer { get; set; }

        /// <summary>
        /// Obtient ou définit le Capitaine de la Team
        /// </summary>
        public virtual Player Captain { get; set; }

        /// <summary>
        /// Obtient ou définit l'Organisateur ayant créé la Team
        /// </summary>
        public virtual Organizer Organizer { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<Play> Plays { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }
    }
}
