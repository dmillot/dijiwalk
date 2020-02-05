//-----------------------------------------------------------------------
// <copyright file="TeamAnswer.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;

    /// <summary>
    /// Class definissant la Réponse d'une Team
    /// </summary>
    public partial class TeamAnswer
    {
        /// <summary>
        /// Obtient ou définit l'Id de la Réponse de la Team
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Team
        /// </summary>
        public int IdTeam { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Jeu de la Réponse de la Team
        /// </summary>
        public int IdGame { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Epreuve correspondant à la Réponse de la Team
        /// </summary>
        public int IdTrial { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Reponse correspondant à l'Epreuve dans les cas ou il y a la choix
        /// </summary>
        public int IdAnswer { get; set; }

        /// <summary>
        /// Obtient ou définit le Text de la Réponse de la Team dans les cas ou il faut ecrire sa reponse
        /// </summary>
        public string TextAnswer { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de la Réponse de la Team
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Obtient ou définit Si la Réponse de la Team est juste
        /// </summary>
        public bool? Good { get; set; }

        /// <summary>
        /// Obtient ou définit la Reponse correspondant à l'Epreuve dans les cas ou il y a la choix
        /// </summary>
        public virtual Answer Answer { get; set; }

        /// <summary>
        /// Obtient ou définit le Jeu de la Réponse de la Team
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Obtient ou définit la Team de la Réponse de la Team
        /// </summary>
        public virtual Team Team { get; set; }

        /// <summary>
        /// Obtient ou définit l'Epreuve correspondant de la Réponse de la Team
        /// </summary>
        public virtual Trial Trial { get; set; }
    }
}
