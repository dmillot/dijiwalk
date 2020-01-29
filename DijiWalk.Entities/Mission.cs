using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Mission
    {
        public Mission()
        {
            Trials = new HashSet<Trial>();
        }

        public int Id { get; set; }
        public int? IdStep { get; set; }
        public string Description { get; set; }
        public int? Score { get; set; }
        public TimeSpan? Time { get; set; }
        public string Name { get; set; }

        public virtual Step Step { get; set; }
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
