using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DijiWalk.Models;
using DijiWalk.Views;

namespace DijiWalk.ViewModels
{
    public class EtapePageViewModel : BindableBase, INavigationAware
    {
        public INavigationService NavigationService { get; private set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MoyenTransport _moyenTransport;
        public MoyenTransport MoyenTransport
        {
            get { return _moyenTransport; }
            set { SetProperty(ref _moyenTransport, value); }
        }

        public DelegateCommand<MoyenTransport> GoToMain { get; set; }

        void GoToMainMethod(MoyenTransport moyenTransport)
        {
            var navParam = new NavigationParameters();
            navParam.Add(nameof(DijiWalk.Models.MoyenTransport), moyenTransport);
            this.NavigationService.NavigateAsync(nameof(MainPage), navParam);
        }

        public EtapePageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            this.GoToMain = new DelegateCommand<MoyenTransport>(GoToMainMethod);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MoyenTransport = parameters[nameof(DijiWalk.Models.MoyenTransport)] as MoyenTransport;
        }


    }
}
