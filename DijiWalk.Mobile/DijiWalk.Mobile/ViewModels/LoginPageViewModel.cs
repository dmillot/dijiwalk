using DijiWalk.Entities;
using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace DijiWalk.Mobile.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToMainPage { get; set; }

        private readonly GameService _gameService;

        #endregion

        public LoginPageViewModel(GameService gameService,INavigationService navigationService)
        {
            NavigationService = navigationService;
            this.NavigateToMainPage = new DelegateCommand<object>(GoToMain);
            _gameService = gameService;

        }


        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se connecter et que ses informations sont correct.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        async void GoToMain(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(MainPage), null);
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
