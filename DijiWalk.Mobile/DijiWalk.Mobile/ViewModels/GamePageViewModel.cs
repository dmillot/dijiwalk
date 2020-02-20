using DijiWalk.Entities;
using DijiWalk.Mobile.Resources.Utils;
using DijiWalk.Mobile.Services;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Mobile.Views;
using DijiWalk.Mobile.Views.PopUp;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DijiWalk.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public INavigationService NavigationService { get; private set; }
        private ViewPlayer _user;

        private ViewStep _actualStep;
        public ViewStep ActualStep
        {
            get { return _actualStep; }
            set
            {
                SetProperty(ref _actualStep, value);
            }
        }

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

        private Dictionary<string, string> _greenColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ValidationColor"]) } };

        private Dictionary<string, string> _redColor = new Dictionary<string, string>() { { "fill=\"\"", GetRGBFromColor.GetRGBFill((Color)Application.Current.Resources["ErrorColor"]) } };

        private readonly PlayerService _playerService;
        public DelegateCommand<object> NavigateToMainPage { get; set; }
        public DelegateCommand<object> NavigateToStepPage { get; set; }
        public DelegateCommand<object> NavigateToLoginPage { get; set; }
        public DelegateCommand<object> PopUpStep { get; set; }


        private ObservableCollection<ViewStep> _steps = new ObservableCollection<ViewStep>();
        public ObservableCollection<ViewStep> Steps
        {
            get { return _steps; }
            set { SetProperty(ref _steps, value); }
        }
        #endregion

        public GamePageViewModel(PlayerService playerService, INavigationService navigationService)
        {
            _playerService = playerService;
            NavigationService = navigationService;
            this.NavigateToMainPage = new DelegateCommand<object>(GoToBack);
            this.NavigateToStepPage = new DelegateCommand<object>(GoToStep);
            this.NavigateToLoginPage = new DelegateCommand<object>(GoToLogin);
            this.PopUpStep = new DelegateCommand<object>(GotToPopUpStep);
        }

        #region NavigationFunction
        /// <summary>
        /// Fonction appelée quand l'utilisateur veut retourner sur la page d'accueil.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GoToBack(object parameters)
        {
            this.NavigationService.NavigateAsync(nameof(MainPage), null);
        }

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

        /// <summary>
        /// Fonction appelée quand l'utilisateur veut plus de détails sur une étape déjà réalisé.
        /// </summary>
        /// <param name="parameters">Command parameter</param>
        void GotToPopUpStep(object parameters)
        {
            var popup = new StepPopUp((ViewStep)parameters);
            PopupNavigation.Instance.PushAsync(popup);
        }
        #endregion


        #region OnNavigatedFunction
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void InitializePage()
        {
            var newObservable = new ObservableCollection<ViewStep>();
            ActualGame.TeamRoutes.Select(tr =>
            {
                var stepCorresponding = ActualGame.Route.RouteSteps.FirstOrDefault(rs => rs.IdStep == tr.IdStep).Step;
                if (ActualStep == null && !(bool)tr.Validate)
                {
                    ActualStep = new ViewStep { Id = tr.StepOrder, Name = stepCorresponding.Name, Description = stepCorresponding.Description, ColorValidation = (bool)tr.Validate ? _greenColor : _redColor, NotFirst = tr.StepOrder == 1 ? false : true, Validation = (bool)tr.Validate, NotLast = ActualGame.TeamRoutes.Last().StepOrder == tr.StepOrder ? false : true };
                }
                newObservable.Add(new ViewStep { Id = tr.StepOrder, Name = stepCorresponding.Name, Description = stepCorresponding.Description, ColorValidation = (bool)tr.Validate ? _greenColor : _redColor, NotFirst = tr.StepOrder == 1 ? false : true, Validation = (bool)tr.Validate, NotLast = ActualGame.TeamRoutes.Last().StepOrder == tr.StepOrder ? false : true });
                return true;
            }).ToList();
            Steps = newObservable;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ActualGame = parameters.GetValue<Game>("allInfo");
            User = parameters.GetValue<ViewPlayer>("user");
            InitializePage(); ;
            #endregion
        }
    }
}