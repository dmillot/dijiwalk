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
using DijiWalk.Mobile.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;

namespace DijiWalk.Mobile.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        private ViewPlayer _user;
        private List<Game> _allInfo;
        private readonly PlayerService _playerService;

        private List<Game> AllInfo
        {
            get { return _allInfo; }
            set { _allInfo = value; }
        }

        public ViewPlayer User 
        { 
            get { return _user; } 
            set 
            {
                SetProperty(ref _user, value);
            } 
        }

        public DelegateCommand<object> NavigateToActualGamePage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }

        private bool _isInGame;
        public bool IsInGame
        {
            get { return _isInGame; }
            set
            {
                SetProperty(ref _isInGame, value);
            }
        }

        private Game _actualGame;
        public Game ActualGame
        {
            get { return _actualGame; }
            set
            {
                SetProperty(ref _actualGame, value);
            }
        }

        private ObservableCollection<Game> _previousGames;
        public ObservableCollection<Game> PreviousGames
        {
            get { return _previousGames; }
            set 
            {
                SetProperty(ref _previousGames, value);
            }
        }

        #endregion

        public MainPageViewModel(PlayerService playerService, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _playerService = playerService;
            this.NavigateToActualGamePage = new DelegateCommand<object>(GoToActualGame);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
        }



        #region NavigationFunction

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page du jeu actuel.
        /// </summary>
        /// <param name="parameters">Id du jeu</param>
        void GoToActualGame(object parameters)
        {
            if (this.IsInGame == true)
            {
                var navigationParams = new NavigationParameters
                {
                    { "user", User },
                    { "allInfo", AllInfo.FirstOrDefault() }
                };
                this.NavigationService.NavigateAsync(nameof(GamePage), navigationParams);
            }
        }



        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se déconnecter.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToLogin(object parameters)
        {
            AllInfo = new List<Game>();
            App.User = null;
            this.NavigationService.NavigateAsync(nameof(LoginPage), null);
        }
        #endregion

        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
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
            AllInfo = await _playerService.GetMobileInfo(this.User.Id);
            PreviousGames = new ObservableCollection<Game>(AllInfo.Where(g => AllInfo.IndexOf(g) != 0));
            var actualGame = AllInfo.Where(g => DateTime.Now >= g.CreationDate && g.FinalTime == null).ToList();
            if (actualGame == null && !actualGame.Any())
                IsInGame = false;
            else
            {
                IsInGame = true;
            }
        }
        #endregion

    }
}
