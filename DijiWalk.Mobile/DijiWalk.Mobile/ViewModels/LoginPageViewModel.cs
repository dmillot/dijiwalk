using DijiWalk.Common.Encryption;
using DijiWalk.Entities;
using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace DijiWalk.Mobile.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToMainPage => new DelegateCommand<object>(GoToMain);

        #region Authentification

        private Login _login;
        public Login Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        #endregion

        private readonly GameService _gameService;
        private readonly AuthentificationService _authentificationService;

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        #endregion

        public LoginPageViewModel(GameService gameService, AuthentificationService authentificationService, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _gameService = gameService;
            _authentificationService = authentificationService;
            Login = new Login();
        }


        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se connecter et que ses informations sont correct.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        public async void GoToMain(object parameters)
        {
            IsLoading = true;
            if (Login.Validate())
            {
                var user = (Player)await _authentificationService.Authentificate(new Player { Login = Login.Pseudo.Value, Password = Login.Password.Value });
                if (user.JwtToken != null)
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("player", user);
                    IsLoading = false;
                    App.User = new ViewPlayer { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Login = user.Login, Picture = user.Picture, TeamPlayers = user.TeamPlayers.Select(tp => new ViewTeamPlayer { IdPlayer = tp.IdPlayer, IdTeam = tp.IdTeam}).ToList() };
                    await this.NavigationService.NavigateAsync(nameof(MainPage), navigationParams);
                }
            }
        }

        #endregion


        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }
        #endregion


    }
}
