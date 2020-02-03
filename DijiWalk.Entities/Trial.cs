//-----------------------------------------------------------------------
// <copyright file="Trial.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant une Epreuve
    /// </summary>
    public partial class Trial
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Epreuve
        /// </summary>
        public Trial()
        {
            Answers = new HashSet<Answer>();
            TeamAnswers = new HashSet<TeamAnswer>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de l'Epreuve
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id du Type de l'Epreuve
        /// </summary>
        public int? IdType { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la Mission de l'Epreuve
        /// </summary>
        public int? IdMission { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de la bonne Reponse à l'Epreuve
        /// </summary>
        public int? IdCorrectAnswer { get; set; }

        /// <summary>
        /// Obtient ou définit le Score de l'Epreuve
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Obtient ou définit le Libelle de l'Epreuve
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Obtient ou définit la bonne Reponse rataché à l'Epreuve
        /// </summary>
        public virtual Answer CorrectAnswer { get; set; }

        /// <summary>
        /// Obtient ou définit la Mission rataché à l'Epreuve
        /// </summary>
        public virtual Mission Mission { get; set; }

        /// <summary>
        /// Obtient ou définit le Type rataché à l'Epreuve
        /// </summary>
        public virtual Type Type { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Reponses propre a l'Epreuve
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Reponses de l'Equipe rataché à l'Epreuve
        /// </summary>
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }
    }
}
