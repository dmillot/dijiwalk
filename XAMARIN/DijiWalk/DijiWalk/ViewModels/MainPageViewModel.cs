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

namespace DijiWalk.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<Jeux> _anciensJeux;
        public ObservableCollection<Jeux> AnciensJeux
        {
            get { return _anciensJeux; }
            set { SetProperty(ref _anciensJeux, value); }
        }

        public DelegateCommand<MoyenTransport> GoToLogin { get; set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            AnciensJeux = new ObservableCollection<Jeux>
            {
                new Jeux {Libelle = "Jeu 2", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 3", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 4", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 1", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 6", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 7", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 8", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 9", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 10", Skin = "jeux1.png", Date = DateTime.Now.Date},
                new Jeux {Libelle = "Jeu 11", Skin = "jeux1.png", Date = DateTime.Now.Date},
            };
            this.GoToLogin = new DelegateCommand<MoyenTransport>(GoToLoginMethod);
        }

        void GoToLoginMethod(MoyenTransport moyenTransport)
        {
            var navParam = new NavigationParameters();
            navParam.Add(nameof(DijiWalk.Models.MoyenTransport), moyenTransport);
            this.NavigationService.NavigateAsync(nameof(LoginPage), navParam);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }


    }
}
