using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class TeamRoute
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public int IdTeam { get; set; }
        public int? IdRoute { get; set; }
        public int? IdStep { get; set; }
        public int StepOrder { get; set; }
        public DateTime? ValidationDate { get; set; }

        public virtual RouteStep RouteStep { get; set; }
        public virtual Game Game { get; set; }
        public virtual Team Team { get; set; }
    }
}
