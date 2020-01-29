using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class RouteTag
    {
        public int IdRoute { get; set; }
        public int IdTag { get; set; }

        public virtual Route Route { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
