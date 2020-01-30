using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DijiWalk.Mobile.ViewModels.ViewEntities
{
    public class ViewTeam : INotifyPropertyChanged { 




        private Boolean _isSelected;


        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCaptain { get; set; }
        public virtual ViewPlayer Captain { get; set; }
        public virtual ICollection<ViewTeamPlayer> TeamPlayers { get; set; }

        public Boolean IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected")); // Throw!!
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
