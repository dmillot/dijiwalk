using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DijiWalk.Entities;

namespace DijiWalk.Mobile.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }


        private ObservableCollection<Game> _anciensJeux;
        public ObservableCollection<Game> AnciensJeux
        {
            get { return _anciensJeux; }
            set { SetProperty(ref _anciensJeux, value); }
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            AnciensJeux = new ObservableCollection<Game>
            {
                new Game {Id = 1, CreationDate = DateTime.Now.Date},
                new Game {Id = 2, CreationDate = DateTime.Now.Date},
                new Game {Id = 3, CreationDate = DateTime.Now.Date},
                new Game {Id = 4, CreationDate = DateTime.Now.Date},
                new Game {Id = 5, CreationDate = DateTime.Now.Date},
                new Game {Id = 6, CreationDate = DateTime.Now.Date},
                new Game {Id = 7, CreationDate = DateTime.Now.Date},
                new Game {Id = 8, CreationDate = DateTime.Now.Date},
                new Game {Id = 9, CreationDate = DateTime.Now.Date},
                new Game {Id = 10, CreationDate = DateTime.Now.Date},
            };
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }

    }
}
