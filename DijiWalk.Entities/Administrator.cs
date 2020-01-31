//-----------------------------------------------------------------------
// <copyright file="Administrator.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class definissant un Administrateur
    /// </summary>
    public partial class Administrator
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'Administrateur
        /// </summary>
        public Administrator()
        {
            this.Organizers = new HashSet<Organizer>();
        }

        /// <summary>
        /// Obtient ou définit l'Id d'un Administrateur
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Prenom d'un Administrateur
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom d'un Administrateur
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Obtient ou définit le Pseudo d'un Administrateur
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Obtient ou définit le Mot de Passe d'un Administrateur
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Obtient ou définit l'Email d'un Administrateur
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Obtient ou définit la liste d'Organisateurs créé par l'Administrateur
        /// </summary>
        public virtual ICollection<Organizer> Organizers { get; set; }
    }
}
