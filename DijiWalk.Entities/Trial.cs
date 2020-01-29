using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Trial
    {
        public Trial()
        {
            Answers = new HashSet<Answer>();
            TeamAnswers = new HashSet<TeamAnswer>();
        }

        public int Id { get; set; }
        public int? IdType { get; set; }
        public int? IdMission { get; set; }
        public int? IdCorrectAnswer { get; set; }
        public int? Score { get; set; }
        public string Libelle { get; set; }

        public virtual Answer CorrectAnswer { get; set; }
        public virtual Mission Mission { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<TeamAnswer> TeamAnswers { get; set; }
    }
}
