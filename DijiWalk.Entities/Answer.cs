using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Answer
    {
        public Answer()
        {
            TeamAnswers = new HashSet<TeamAnswer>();
            Trials = new HashSet<Trial>();
        }

        public int Id { get; set; }
        public int? IdTrial { get; set; }
        public string Libelle { get; set; }
        public string Picture { get; set; }

        public virtual Trial Trial { get; set; }
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
