using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Team
    {
        public Team()
        {
            Plays = new HashSet<Play>();
            TeamAnswers = new HashSet<TeamAnswer>();
            TeamPlayers = new HashSet<TeamPlayer>();
            TeamRoutes = new HashSet<TeamRoute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCaptain { get; set; }
        public int? IdOrganizer { get; set; }

        public virtual Player Captain { get; set; }
        public virtual Organizer Organizer { get; set; }
        public virtual ICollection<Play> Plays { get; set; }
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }
    }
}
