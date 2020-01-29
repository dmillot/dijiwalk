using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Organizer
    {
        public Organizer()
        {
            Games = new HashSet<Game>();
            Messages = new HashSet<Message>();
            Players = new HashSet<Player>();
            Routes = new HashSet<Route>();
            Teams = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string OrganizerEmail { get; set; }
        public int? IdAdministrator { get; set; }

        public virtual Administrator Administrator { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
