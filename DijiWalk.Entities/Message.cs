using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int IdOrganizer { get; set; }
        public string Content { get; set; }
        public bool FromOrganiser { get; set; }
        public DateTime? Date { get; set; }

        public virtual Organizer Organizer { get; set; }
        public virtual Player Player { get; set; }
    }
}
