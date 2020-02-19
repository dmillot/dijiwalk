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
        public DelegateCommand<object> NavigateToChatPage { get; set; }
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
        #endregion
        public EtapePageViewModel(INavigationService navigationService, IStepService stepService, IPlayerService playerService)
        {

            this._playerService = playerService;
            this.NavigationService = navigationService;
            this._stepService = stepService;

            this.NavigateToQuizzPage = new DelegateCommand<object>(GoToQuizz);
            this.NavigateToValidationPage = new DelegateCommand<object>(GoToValidation);
            this.NavigateToChatPage = new DelegateCommand<object>(GoToChat);
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
            this.NavigationService.NavigateAsync(nameof(QuizzPage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut valider l'étape actuelle.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToValidation(object parameters)
        {
            var navigationParams = new NavigationParameters
            {
                { "step", ActualStep }
            };

            this.NavigationService.NavigateAsync(nameof(ValidationPage), navigationParams);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur la page de chat.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToChat(object parameters)
        {
            //this.NavigationService.NavigateAsync(nameof(ChatPage), null); TO DO /////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se déconnecter.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToLogin(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(LoginPage), null);
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut retourner sur la page du jeu.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToGame(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(GamePage), null);
        }
        #endregion

        #region OnNavigatedFunction

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var response = await _playerService.GetCurrentStep(App.User.Id);
            this.ActualStep = new ViewStep
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                CreationDate = response.CreationDate,
                Clues = response.Clues
            };
        }

        #endregion
    }
}
