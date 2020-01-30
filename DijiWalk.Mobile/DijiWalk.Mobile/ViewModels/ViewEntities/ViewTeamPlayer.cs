using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DijiWalk.Mobile.ViewModels.ViewEntities
{
    public class ViewTeamPlayer : INotifyPropertyChanged
    {
        private Boolean _isCapitaine;

        public int IdTeam { get; set; }
        public int IdPlayer { get; set; }

        public virtual ViewPlayer Player { get; set; }
        public virtual ViewTeam Team { get; set; }

        public Boolean IsCapitaine
        {
            get { return _isCapitaine; }
            set
            {
                _isCapitaine = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsCapitaine")); // Throw!!
                }
            }
        }

      


        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
