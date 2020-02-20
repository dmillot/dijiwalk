//-----------------------------------------------------------------------
// <copyright file="StepValidation.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Class of validation step
    /// </summary>
    public partial class StepValidation
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une StepValidation
        /// </summary>
        public StepValidation()
        {
           
        }

        /// <summary>
        /// Obtient ou définit l'Id d'une StepValidation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'étape
        /// </summary>
        public int IdStep { get; set; }

        /// <summary>
        /// Obtient ou définit la Description d'une StepValidation
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit le score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Obtient ou définit l'étape
        /// </summary>
        public virtual Step Step { get; set; }
    }
}
