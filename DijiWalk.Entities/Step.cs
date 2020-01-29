using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Step
    {
        public Step()
        {
            Missions = new HashSet<Mission>();
            RouteSteps = new HashSet<RouteStep>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Validation { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Name { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
        public virtual ICollection<RouteStep> RouteSteps { get; set; }
    }
}
