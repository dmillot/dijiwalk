//-----------------------------------------------------------------------
// <copyright file="Answer.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant une Réponse
    /// </summary>
    public partial class Answer
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Réponse
        /// </summary>
        public Answer()
        {
            this.TeamAnswers = new HashSet<TeamAnswer>();
            this.Trials = new HashSet<Trial>();
        }

        /// <summary>
        /// Obtient ou définit l'Id d'une Réponse
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Question rataché à la Réponse
        /// </summary>
        public int? IdTrial { get; set; }

        /// <summary>
        /// Obtient ou définit le Contenu de la Réponse (peut etre null en fonction du type de Question/Reponse)
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Obtient ou définit le lien de la Photo de la Réponse (peut etre null en fonction du type de Question/Reponse)
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Obtient ou définit la Question rataché à la Réponse
        /// </summary>
        public virtual Trial Trial { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Reponses des Equipes rataché à la Réponse
        /// </summary>
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
