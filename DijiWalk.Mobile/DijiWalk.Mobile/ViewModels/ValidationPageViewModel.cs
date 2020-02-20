﻿using DijiWalk.Common.Contracts;
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

        public ValidationPageViewModel(INavigationService navigationService, IValidationService validationService, IPlayerService playerService, IStringExtension stringExtension)
        {
            this.NavigationService = navigationService;
            this._validationService = validationService;
            this._playerService = playerService;
            this._stringExtension = stringExtension;
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
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
            var navigationParams = new NavigationParameters
            {
                { "user", User },
                { "allInfo", ActualGame },
                { "step", ActualStep }
            };
            this.NavigationService.NavigateAsync(nameof(EtapePage), navigationParams);
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
        #endregion


        public Game Game { get; set; }
        public Player Player { get; set; }
        public Step Step { get; set; }

        public async void Validate(object parameters)
        {
            var filename = $"{ActualStep.Name}-jeu{ActualGame.Id}-team{ActualGame.Plays.FirstOrDefault().Team.Name}-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";

            byte[] imageArray = System.IO.File.ReadAllBytes(Photo.Path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            Validate validate = new Validate
            {
                IdGame = ActualGame.Id,
                IdPlayer = User.Id,
                Filename = filename,
                Picture = String.Concat("data:image/jpg;base64,",base64ImageRepresentation),
                IdStep = ActualStep.Id,
                IdTeam = ActualGame.Plays.FirstOrDefault().IdTeam
            };

            var uploadedImage = await _validationService.ValidationImage(validate);

        }

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
