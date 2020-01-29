using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Type
    {
        public Type()
        {
            Trials = new HashSet<Trial>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Trial> Trials { get; set; }
    }
}
