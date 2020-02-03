//-----------------------------------------------------------------------
// <copyright file="Mission.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant une Mission
    /// </summary>
    public partial class Mission
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Mission
        /// </summary>
        public Mission()
        {
            Trials = new HashSet<Trial>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de la Mission
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Etape de la Mission
        /// </summary>
        public int? IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit le Contenu de la Mission
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit le Score de la Mission
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Obtient ou définit le Temps de la Mission
        /// </summary>
        public TimeSpan? Time { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom de la Mission
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtient ou définit l'Etape de la Mission
        /// </summary>
        public virtual Step Step { get; set; }

        /// <summary>
        /// Obtient ou définit la liste d'Epreuve de la Mission
        /// </summary>
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
