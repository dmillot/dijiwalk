using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Player
    {
        public Player()
        {
            Messages = new HashSet<Message>();
            Teams = new HashSet<Team>();
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public int? IdOrganizer { get; set; }

        public virtual Organizer Organizer { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
