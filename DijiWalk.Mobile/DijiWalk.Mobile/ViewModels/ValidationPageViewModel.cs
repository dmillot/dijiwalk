using DijiWalk.Common.Contracts;
using DijiWalk.Entities;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Mobile.Views;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace DijiWalk.Mobile.ViewModels
{
    public class ValidationPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToChatPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        public DelegateCommand ValidatePhoto { get; set; }

        private readonly IValidationService _validationService;
        private readonly IPlayerService _playerService;
        private readonly IStringExtension _stringExtension;
        public MediaFile Photo;

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

        public ValidationPageViewModel(INavigationService navigationService, IValidationService validationService, IPlayerService playerService, IStringExtension stringExtension)
        {
            this.NavigationService = navigationService;
            this._validationService = validationService;
            this._playerService = playerService;
            this._stringExtension = stringExtension;
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
            this.NavigateToChatPage = new DelegateCommand<object>(GoToChat);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
            this.ValidatePhoto = new DelegateCommand(Validate);
        }

        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut aller sur le détails de l'étape actuelle.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToStep(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(EtapePage), null);
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
        #endregion

        public async void Validate()
        {
            var game = await _playerService.GetActualGame(App.User.Id);


            var filename = _stringExtension.RemoveDiacriticsAndWhiteSpace(ActualStep.Name + "-jeu" + game.Id + DateTime.Now);

            byte[] imageArray = System.IO.File.ReadAllBytes(Photo.Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            var uploadedImage = await _validationService.UploadImage(base64ImageRepresentation, filename);
        }

        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var response = parameters.GetValue<Step>("step");
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
