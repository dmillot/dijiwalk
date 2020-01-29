using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Game
    {
        public Game()
        {
            Plays = new HashSet<Play>();
            TeamAnswers = new HashSet<TeamAnswer>();
            TeamRoutes = new HashSet<TeamRoute>();
        }

        public int Id { get; set; }
        public int? IdRoute { get; set; }
        public DateTime? FinalTime { get; set; }
        public int? FinalScore { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? IdOrganizer { get; set; }
        public int? IdTransport { get; set; }

        public virtual Organizer Organizer { get; set; }
        public virtual Route Route { get; set; }
        public virtual Transport Transport { get; set; }
        public virtual ICollection<Play> Plays { get; set; }
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }
    }
}
