//-----------------------------------------------------------------------
// <copyright file="Type.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant un Type
    /// </summary>
    public partial class Type
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'un Type
        /// </summary>
        public Type()
        {
            Trials = new HashSet<Trial>();
        }

        /// <summary>
        /// Obtient ou définit l'Id d'un Type
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit lq Description d'un Type
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Epreuves rataché au Type
        /// </summary>
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
