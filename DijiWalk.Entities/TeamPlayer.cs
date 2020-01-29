using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class TeamPlayer
    {
        public int IdTeam { get; set; }
        public int IdPlayer { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
