using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.ViewModels.ViewEntities;
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
        private readonly PlayerService _playerService;
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToQuizzPage { get; set; }
        public DelegateCommand<object> NavigateToValidationPage { get; set; }
        public DelegateCommand<object> NavigateToChatPage { get; set; }
        public DelegateCommand<object> NavigateToGamePage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }

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


        public EtapePageViewModel(PlayerService playerService, INavigationService navigationService)
        {
            this._playerService = playerService;
            this.NavigationService = navigationService;
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
            //this.NavigationService.NavigateAsync(nameof(QuizzPage), null); TO DO /////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut valider l'étape actuelle.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToValidation(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(ValidationPage), null);
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
            Console.WriteLine(response);
            //ViewStep test = new ViewStep
            //{
            //    Id = response.Id,
            //    Name = response.Name
            //};
        }

        #endregion

    }
}
