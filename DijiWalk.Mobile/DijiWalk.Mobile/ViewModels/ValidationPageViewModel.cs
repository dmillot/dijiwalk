using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.ViewModels
{
    public class ValidationPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; private set; }


        public ValidationPageViewModel(INavigationService navigationService)
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
