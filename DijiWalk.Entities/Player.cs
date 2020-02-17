
//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace DijiWalk.Entities
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    /// <summary>
    /// Class definissant un Joueur
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Constructeur d'une nouvelle instance d'un Joueur
        /// </summary>
        public Player()
        {
            Messages = new HashSet<Message>();
            Teams = new HashSet<Team>();
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        /// <summary>
        /// Obtient ou définit l'Id du Joueur
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le Prenom du Joueur
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Obtient ou définit le Nom du Joueur
        /// </summary>
        public string LastName { get; set; }

       
        /// <summary>
        /// Obtient ou définit le Pseudo du Joueur
        /// </summary>
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }
       
        /// <summary>
        /// Obtient ou définit le Mot de Passe du Joueur
        /// </summary>
         [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Obtient ou définit l'Email du Joueur
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Obtient ou définit la Photo du Joueur
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Obtient ou définit l'Id de l'Organisateur ayant créé le Joueur
        /// </summary>
        public int? IdOrganizer { get; set; }

        [NotMapped]
        public string ImageBase64 { get; set; }

        [NotMapped]
        public bool ImageChanged { get; set; }

        [NotMapped]
        public bool PasswordChanged { get; set; }

        /// <summary>
        /// Obtient ou définit l'Organisateur ayant créé le Joueur
        /// </summary>

        public virtual Organizer Organizer { get; set; }

        /// <summary>
        /// Obtient ou définit la liste de Message envoyé et recu par le Joueur
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// Obtient ou définit la liste d'Equipe auquel le Joueur a fait partis
        /// </summary>
        public virtual ICollection<Team> Teams { get; set; }

        /// <summary>
        /// Liste Généré par la BDD inutile pour nous mais à garder (même utilité que celle d'avant)
        /// </summary>
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }

        [NotMapped]
        public JWTTokens JwtToken { get; set; }


    }
}
