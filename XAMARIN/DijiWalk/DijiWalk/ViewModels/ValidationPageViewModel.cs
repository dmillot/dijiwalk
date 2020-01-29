using DijiWalk.Models;
using DijiWalk.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijiWalk.ViewModels
{
    public class ValidationPageViewModel : BindableBase, INavigationAware
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

        public DelegateCommand<MoyenTransport> GoToLogin { get; set; }

        public ValidationPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            MoyenTransport = new MoyenTransport { Id = 1, Libelle = "Piétons" };
            this.GoToLogin = new DelegateCommand<MoyenTransport>(GoToLoginMethod);
        }

        void GoToLoginMethod(MoyenTransport moyenTransport)
        {
            var navParam = new NavigationParameters();
            navParam.Add(nameof(DijiWalk.Models.MoyenTransport), moyenTransport);
            this.NavigationService.NavigateAsync(nameof(LoginPage), navParam);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Any())
            {
                MoyenTransport = parameters[nameof(DijiWalk.Models.MoyenTransport)] as MoyenTransport;
            }

        }
    }
}
