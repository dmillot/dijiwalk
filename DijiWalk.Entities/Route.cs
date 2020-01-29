using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Route
    {
        public Route()
        {
            Games = new HashSet<Game>();
            RouteSteps = new HashSet<RouteStep>();
            RouteTags = new HashSet<RouteTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Handicap { get; set; }
        public TimeSpan? Time { get; set; }
        public string Distance { get; set; }
        public int? IdOrganizer { get; set; }

        public virtual Organizer Organizer { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<RouteStep> RouteSteps { get; set; }
        public virtual ICollection<RouteTag> RouteTags { get; set; }
    }
}
