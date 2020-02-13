//-----------------------------------------------------------------------
// <copyright file="Transport.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class definissant un Moyen de Transport
    /// </summary>
    public partial class Transport
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'un Transport
        /// </summary>
        public Transport()
        {
            Games = new HashSet<Game>();
        }

        /// <summary>
        /// Obtient ou définit l'Id du Transport
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Libelle du Transport
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Obtient ou définit la Description du Transport
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Jeux utilisant le Transport
        /// </summary>
        public virtual ICollection<Game> Games { get; set; }
    }
}
