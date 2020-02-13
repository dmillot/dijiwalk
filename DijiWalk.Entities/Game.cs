//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class definissant un Jeu
    /// </summary>
    public partial class Game
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'un Jeu
        /// </summary>
        public Game()
        {
            Plays = new HashSet<Play>();
            TeamAnswers = new HashSet<TeamAnswer>();
            TeamRoutes = new HashSet<TeamRoute>();
        }

        public Game(Game game)
        {
            Id = game.Id;
            IdRoute = game.IdRoute;
            CreationDate = game.CreationDate;
            IdOrganizer = game.IdOrganizer;
            IdTransport = game.IdTransport;
            Plays = new HashSet<Play>();
            TeamAnswers = new HashSet<TeamAnswer>();
            TeamRoutes = new HashSet<TeamRoute>();
        }

       

        /// <summary>
        /// Obtient ou définit l'Id du Jeu
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Route du Jeu
        /// </summary>
        public int? IdRoute { get; set; }

        /// <summary>
        /// Obtient ou définit la Durée total du Jeu
        /// </summary>
        public DateTime? FinalTime { get; set; }

        /// <summary>
        /// Obtient ou définit le Score final du Jeu    ?????
        /// </summary>
        public int? FinalScore { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de creation du Jeu
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Organisateur du Jeu
        /// </summary>
        public int? IdOrganizer { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Transport du Jeu
        /// </summary>
        public int? IdTransport { get; set; }

        /// <summary>
        /// Obtient ou définit l'Organisateur du Jeu
        /// </summary>
        public virtual Organizer Organizer { get; set; }

        /// <summary>
        /// Obtient ou définit la Route du Jeu
        /// </summary>
        public virtual Route Route { get; set; }

        /// <summary>
        /// Obtient ou définit le Transport du Jeu
        /// </summary>
        public virtual Transport Transport { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Equipes participant au Jeu
        /// </summary>
        public virtual ICollection<Play> Plays { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Réponses de chaque Teams du Jeu
        /// </summary>
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Routes pour chaques Teams du Jeu
        /// </summary>
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }


    }
}
