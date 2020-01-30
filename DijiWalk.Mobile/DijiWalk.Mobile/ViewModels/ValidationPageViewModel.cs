using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.ViewModels
{
    public class ValidationPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToChatPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        #endregion

        public ValidationPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
            this.NavigateToChatPage = new DelegateCommand<object>(GoToChat);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
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
