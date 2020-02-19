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
using System.Linq;
using System.Threading.Tasks;

namespace DijiWalk.Mobile.ViewModels
{
    public class ValidationPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToChatPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        public DelegateCommand<object> ValidatePhoto { get; set; }

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
            this.ValidatePhoto = new DelegateCommand<object>(Validate);
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


        public Game Game { get; set; }
        public Player Player { get; set; }
        public Step Step { get; set; }

        public async void Validate(object parameters)
        {

            //var test = 1;

            //var idTeamsOfPlayer = Player.TeamPlayers.Select(t => t.IdTeam).ToList();

            //var test1 = Step.Name;
            //var test2 = Game.Id;

            //var filename = _stringExtension.RemoveDiacriticsAndWhiteSpace(Step.Name + "-jeu" + Game.Id + DateTime.Now);

            //byte[] imageArray = System.IO.File.ReadAllBytes(Photo.Path);
            //string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            //Validate validate = new Validate
            //{
            //    IdGame = Game.Id,
            //    IdPlayer = App.User.Id,
            //    Filename = filename,
            //    Picture = base64ImageRepresentation,
            //    IdStep = Step.Id,
            //    IdTeam = Game.Plays.Where(g => idTeamsOfPlayer.Contains(g.IdTeam)).FirstOrDefault().IdTeam
            //};

            //var uploadedImage = await _validationService.ValidationImage(validate);
        }

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
