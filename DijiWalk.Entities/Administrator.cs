using System;
using System.Collections.Generic;

namespace DijiWalk.Entities
{
    public partial class Administrator
    {
        public Administrator()
        {
            Organizers = new HashSet<Organizer>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Organizer> Organizers { get; set; }
    }
}
