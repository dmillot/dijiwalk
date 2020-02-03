//-----------------------------------------------------------------------
// <copyright file="Organizer.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant un Organisateur
    /// </summary>
    public partial class Organizer
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'Organisateur
        /// </summary>
        public Organizer()
        {
            Games = new HashSet<Game>();
            Messages = new HashSet<Message>();
            Players = new HashSet<Player>();
            Routes = new HashSet<Route>();
            Teams = new HashSet<Team>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de l'Organisateur
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Prenom de l'Organisateur
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom de l'Organisateur
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Obtient ou définit le Pseudo de l'Organisateur
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Obtient ou définit le Mot de Passe de l'Organisateur
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Obtient ou définit l'Email de l'Organisateur
        /// </summary>
        public string OrganizerEmail { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'dAdministrateur ayant aprouvé l'Organisateur
        /// </summary>
        public int? IdAdministrator { get; set; }

        /// <summary>
        /// Obtient ou définit l'Administrateur ayant aprouvé l'Organisateur
        /// </summary>
        public virtual Administrator Administrator { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Jeu créé par l'Organisateur
        /// </summary>
        public virtual ICollection<Game> Games { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Message envoyé et recu par l'Organisateur
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Joueur créé par l'Organisateur
        /// </summary>
        public virtual ICollection<Player> Players { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Routes définis par l'Organisateur
        /// </summary>
        public virtual ICollection<Route> Routes { get; set; }

        /// <summary>
        /// Obtient ou définit la liste d'Equipe créé par l'Organisateur
        /// </summary>
        public virtual ICollection<Team> Teams { get; set; }
    }
}
