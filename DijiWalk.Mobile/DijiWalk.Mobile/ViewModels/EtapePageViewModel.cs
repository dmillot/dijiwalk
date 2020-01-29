using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Mobile.ViewModels
{
    public class EtapePageViewModel : BindableBase, INavigationAware
    {
        public INavigationService NavigationService { get; private set; }


        public EtapePageViewModel(INavigationService navigationService)
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
