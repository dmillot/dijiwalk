using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DijiWalk.Models;
using DijiWalk.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Globalization;
using DijiWalk.Ressources.Converters;

namespace DijiWalk.ViewModels
{
    public class WaitingPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }

       
        ObservableCollection<Groupe> groupes = new ObservableCollection<Groupe>();
        public ObservableCollection<Groupe> Groupes { get { return groupes; } }


        //public List<Groupe> Groupes { get; set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MoyenTransport _moyenTransport;
        public MoyenTransport MoyenTransport
        {
            get { return _moyenTransport; }
            set { SetProperty(ref _moyenTransport, value); }
        }

        public DelegateCommand<MoyenTransport> GoToMain { get; set; }

        void GoToMainMethod(MoyenTransport moyenTransport)
        {
            var navParam = new NavigationParameters();
            navParam.Add(nameof(DijiWalk.Models.MoyenTransport), moyenTransport);
            this.NavigationService.NavigateAsync(nameof(MainPage), navParam);
        }

        public WaitingPageViewModel(INavigationService navigationService)
        {

            groupes = new ObservableCollection<Groupe>() 
            { 
            new Groupe { Nom = "Groupe 1", status = true},
            new Groupe { Nom = "Groupe 2", status = true},
            new Groupe { Nom = "Groupe 3", status = false},
            new Groupe { Nom = "Groupe 4", status = true},
            new Groupe { Nom = "Groupe 5", status = true},
            new Groupe { Nom = "Groupe 6", status = false},
            new Groupe { Nom = "Groupe 7", status = true},
            new Groupe { Nom = "Groupe 8", status = true},
            new Groupe { Nom = "Groupe 9", status = true},
            new Groupe { Nom = "Groupe 10", status = true},
            new Groupe { Nom = "Groupe 11", status = false},
            new Groupe { Nom = "Groupe 12", status = true},
            new Groupe { Nom = "Groupe 13", status = false},
            new Groupe { Nom = "Groupe 14", status = true},
            new Groupe { Nom = "Groupe 15", status = false},
            new Groupe { Nom = "Groupe 16", status = true},
            new Groupe { Nom = "Groupe 17", status = false},
            new Groupe { Nom = "Groupe 18", status = true},
            new Groupe { Nom = "Groupe 19", status = false},
            new Groupe { Nom = "Groupe 20", status = true},
            };

            NavigationService = navigationService;
            this.GoToMain = new DelegateCommand<MoyenTransport>(GoToMainMethod);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MoyenTransport = parameters[nameof(DijiWalk.Models.MoyenTransport)] as MoyenTransport;
        }

    
    }
}
