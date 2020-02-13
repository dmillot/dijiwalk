//-----------------------------------------------------------------------
// <copyright file="Step.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.Entities
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class definissant une Etape
    /// </summary>
    public partial class Step
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'une Etape
        /// </summary>
        public Step()
        {
            _additionalData = new Dictionary<string, JToken>();
            Missions = new HashSet<Mission>();
            RouteSteps = new HashSet<RouteStep>();
        }

        public Step(Step s)
        {
            Id = s.Id;
            Description = s.Description;
            Validation = s.Validation;
            CreationDate = s.CreationDate;
            Name = s.Name;
            Lat = s.Lat;
            Lng = s.Lng;
            ImageBase64 = s.ImageBase64;
            ImageChanged = s.ImageChanged;
            _additionalData = new Dictionary<string, JToken>();
            Missions = new HashSet<Mission>();
            RouteSteps = new HashSet<RouteStep>();
            StepTags = new HashSet<StepTag>();
        }

        /// <summary>
        /// Obtient ou définit l'Id de l'Etape
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit la Description de l'Etape
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtient ou définit la Validation de l'Etape
        /// </summary>
        public string Validation { get; set; }

        /// <summary>
        /// Obtient ou définit la Date de Creation de l'Etape
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom de l'Etape
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtient ou définit la Latitude de l'Etape
        /// </summary>
        public double? Lat { get; set; }

        /// <summary>
        /// Obtient ou définit la Longitude de l'Etape
        /// </summary>
        public double? Lng { get; set; }

        [NotMapped]
        public string ImageBase64 { get; set; }

        [NotMapped]
        public bool ImageChanged { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<Mission> Missions { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder
        /// </summary>
        public virtual ICollection<RouteStep> RouteSteps { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des Tags d'une étape
        /// </summary>
        public virtual ICollection<StepTag> StepTags { get; set; }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Lat = Convert.ToDouble(_additionalData["Latitude"]);
            Lng = Convert.ToDouble(_additionalData["Longitude"]);


        }
    }
}
