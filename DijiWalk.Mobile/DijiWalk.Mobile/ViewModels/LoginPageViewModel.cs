using Prism.Mvvm;
using Prism.Navigation;

namespace DijiWalk.Mobile.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }

        public LoginPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }


    }
}
