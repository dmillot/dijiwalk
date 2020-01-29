using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            RouteTags = new HashSet<RouteTag>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RouteTag> RouteTags { get; set; }
    }
}
