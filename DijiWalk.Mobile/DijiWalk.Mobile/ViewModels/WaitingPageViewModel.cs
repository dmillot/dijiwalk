using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DijiWalk.Entities;

namespace DijiWalk.Mobile.ViewModels
{
    public class WaitingPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }


        private ObservableCollection<Team> _groupes = new ObservableCollection<Team>();
        public ObservableCollection<Team> Groupes { 
            get { return _groupes; }
            set { SetProperty(ref _groupes, value); }
        }

        public Boolean Status
        {
            get { return false; }
        }


        public WaitingPageViewModel(INavigationService navigationService)
        {

            Groupes = new ObservableCollection<Team>()
            {
            new Team { Name = "Groupe 1"},
            new Team { Name = "Groupe 2"},
            new Team { Name = "Groupe 3"},
            new Team { Name = "Groupe 4"},
            new Team { Name = "Groupe 5" },
            new Team { Name = "Groupe 6"},
            new Team { Name = "Groupe 7"},
            new Team { Name = "Groupe 8"},
            new Team { Name = "Groupe 9"},
            new Team { Name = "Groupe 10"},
            new Team { Name = "Groupe 11"},
            new Team { Name = "Groupe 12"},
            new Team { Name = "Groupe 13"},
            new Team { Name = "Groupe 14"},
            new Team { Name = "Groupe 15"},
            new Team { Name = "Groupe 16"},
            new Team { Name = "Groupe 17"},
            new Team { Name = "Groupe 18"},
            new Team { Name = "Groupe 19"},
            new Team { Name = "Groupe 20"},
            };

            NavigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }


    }
}
