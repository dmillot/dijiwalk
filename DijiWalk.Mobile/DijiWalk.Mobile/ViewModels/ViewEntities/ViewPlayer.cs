using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.ViewModels.ViewEntities
{
    public class ViewPlayer 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Picture { get; set; }
        public virtual ICollection<ViewTeam> Teams { get; set; }
        public virtual ICollection<ViewTeamPlayer> TeamPlayers { get; set; }
        public string NameAndPseudo
        {
            get { return FirstName + " " + LastName + " / " + Login; }
        }
    }
}
