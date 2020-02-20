using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Entities;
using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DijiWalk.Mobile.ViewModels
{
    public class EtapePageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        private readonly IPlayerService _playerService;
        private readonly IStepService _stepService;
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToQuizzPage { get; set; }
        public DelegateCommand<object> NavigateToValidationPage { get; set; }
        public DelegateCommand<object> NavigateToGamePage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        public Step Step { get; set; }

        private ViewStep _actualStep;
        public ViewStep ActualStep
        {
            get { return _actualStep; }
            set
            {
                SetProperty(ref _actualStep, value);
            }
        }

        private ViewPlayer _user;
        private Game _actualGame;
        public Game ActualGame
        {
            get { return _actualGame; }
            set
            {
                SetProperty(ref _actualGame, value);
            }
        }

        public ViewPlayer User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        #endregion
        public EtapePageViewModel(INavigationService navigationService, IStepService stepService, IPlayerService playerService)
        {

            this._playerService = playerService;
            this.NavigationService = navigationService;
            this._stepService = stepService;

            this.NavigateToQuizzPage = new DelegateCommand<object>(GoToQuizz);
            this.NavigateToValidationPage = new DelegateCommand<object>(GoToValidation);
            this.NavigateToGamePage = new DelegateCommand<object>(GoToGame);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
        }

        #region NavigationFunction

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page du quizz.
        /// </summary>
        /// <param name="parameters">Id quizz</param>
        void GoToQuizz(object parameters)
        {
            var navigationParams = new NavigationParameters
            {
                { "user", User },
                { "allInfo", ActualGame },
                { "step", ActualStep }
            };
            this.NavigationService.NavigateAsync(nameof(QuizzPage), navigationParams);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut valider l'étape actuelle.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToValidation(object parameters)
        {
            var navigationParams = new NavigationParameters
            {
                { "user", User },
                { "allInfo", ActualGame },
                { "step", ActualStep }
            };
            this.NavigationService.NavigateAsync(nameof(ValidationPage), navigationParams);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se déconnecter.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToLogin(object parameters)
        {
            App.User = null;
            this.NavigationService.NavigateAsync(nameof(LoginPage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut retourner sur la page du jeu.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToGame(object parameters)
        {
            var navigationParams = new NavigationParameters
            {
                { "user", User },
                { "allInfo", ActualGame }
            };
            this.NavigationService.NavigateAsync(nameof(GamePage), navigationParams);
        }
        #endregion

        #region OnNavigatedFunction

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ActualGame = parameters.GetValue<Game>("allInfo");
            User = parameters.GetValue<ViewPlayer>("user");
            ActualStep = parameters.GetValue<ViewStep>("step");
        }

        #endregion
    }
}
