//-----------------------------------------------------------------------
// <copyright file="Clue.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    /// <summary>
    /// Class definissant un Indice
    /// </summary>
    public partial class Clue
    {
        /// <summary>
        /// Obtient ou définit l'Id de l'Indice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit la Description de l'Indice
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Etape
        /// </summary>
        public int IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit l'Etape qui est rataché à l'Indice
        /// </summary>
        public virtual Step Step { get; set; }
    }
}
