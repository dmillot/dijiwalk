using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class RouteStep
    {
        public RouteStep()
        {
            TeamRoutes = new HashSet<TeamRoute>();
        }

        public int IdRoute { get; set; }
        public int IdStep { get; set; }
        public int? Order { get; set; }

        public virtual Route Route { get; set; }
        public virtual Step Step { get; set; }
        public virtual ICollection<TeamRoute> TeamRoutes { get; set; }
    }
}
