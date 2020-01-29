using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Play
    {
        public int IdGame { get; set; }
        public int IdTeam { get; set; }
        public int? Score { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? Time { get; set; }

        public virtual Game Game { get; set; }
        public virtual Team Team { get; set; }
    }
}
