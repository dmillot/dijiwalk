using DijiWalk.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace DijiWalk.Mobile.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        public INavigationService NavigationService { get; private set; }
        public DelegateCommand<object> NavigateToMainPage { get; set; }
       

        #endregion

        public LoginPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            this.NavigateToMainPage = new DelegateCommand<object>(GoToMain);
        }

        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut se connecter et que ses informations sont correct.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToMain(object parameters)
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
