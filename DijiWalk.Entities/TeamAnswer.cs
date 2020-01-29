using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class TeamAnswer
    {
        public int Id { get; set; }
        public int IdTeam { get; set; }
        public int IdGame { get; set; }
        public int IdTrial { get; set; }
        public int IdAnswer { get; set; }
        public string TextAnswer { get; set; }
        public DateTime? Date { get; set; }
        public bool? Good { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Game Game { get; set; }
        public virtual Team Team { get; set; }
        public virtual Trial Trial { get; set; }
    }
}
