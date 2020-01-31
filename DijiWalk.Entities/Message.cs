//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;

    /// <summary>
    /// Class definissant un Message
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// Obtient ou définit l'Id d'un Message
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Joueur concerné par le Message
        /// </summary>
        public int IdPlayer { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Organisateur concerné par le Message
        /// </summary>
        public int IdOrganizer { get; set; }

        /// <summary>
        /// Obtient ou définit le Contenu du Message
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Obtient ou définit si le Message vient de l'Administrateur
        /// </summary>
        public bool FromOrganiser { get; set; }

        /// <summary>
        /// Obtient ou définit la Date du Message
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Obtient ou définit l'Organisateur concerné par le Message
        /// </summary>
        public virtual Organizer Organizer { get; set; }

        /// <summary>
        /// Obtient ou définit le Joueur concerné par le Message
        /// </summary>
        public virtual Player Player { get; set; }
    }
}
