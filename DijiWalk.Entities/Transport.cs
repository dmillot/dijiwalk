using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Transport
    {
        public Transport()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
