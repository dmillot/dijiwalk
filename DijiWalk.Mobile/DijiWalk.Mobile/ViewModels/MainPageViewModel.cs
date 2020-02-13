using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DijiWalk.Entities;
using Prism.Commands;
using DijiWalk.Mobile.Views;
using DijiWalk.Mobile.ViewModels.ViewEntities;

namespace DijiWalk.Mobile.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {


        //ChangeColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFill(Color.Blue) } }; EXEMPLE REPLACESTRINGMAP SVG

        #region Properties
        public INavigationService NavigationService { get; private set; }
        private ViewPlayer _user;
        public ViewPlayer User { get { return _user; } set { SetProperty(ref _user, value); } }
        public DelegateCommand<object> NavigateToGamePage { get; set; }
        public DelegateCommand<object> NavigateToActualGamePage { get; set; }
        public DelegateCommand<object> NavigateToClassementPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }


        private ObservableCollection<Game> _anciensJeux;
        public ObservableCollection<Game> AnciensJeux
        {
            get { return _anciensJeux; }
            set { SetProperty(ref _anciensJeux, value); }
        }
        #endregion

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
            this.NavigateToGamePage = new DelegateCommand<object>(GoToGame);
            this.NavigateToActualGamePage = new DelegateCommand<object>(GoToActualGame);
            this.NavigateToClassementPage = new DelegateCommand<object>(GoToClassement);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
        }



        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page des classement.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToClassement(object parameters)
        {
            //this.NavigationService.NavigateAsync(nameof(MainPage), null); TO DO //////////////////////////////////////////////////////////
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page du jeu actuel.
        /// </summary>
        /// <param name="parameters">Id du jeu</param>
        void GoToActualGame(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(GamePage), null);
        }


        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page d'un jeu (pas forcément actuel).
        /// </summary>
        /// <param name="parameters">Id du jeu</param>
        void GoToGame(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(GamePage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se déconnecter.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToLogin(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(LoginPage), null);
        }
        #endregion

        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (App.User == null)
            {
                var response = parameters.GetValue<Player>("player");

                var user = new ViewPlayer
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    Picture = response.Picture,
                    Login = response.Login
                };

                App.User = user;
            }

            this.User = App.User;

        }
        #endregion

    }
}
