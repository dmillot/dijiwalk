using Prism;
using Prism.Ioc;
using DijiWalk.Mobile.ViewModels;
using DijiWalk.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.Services;
using Rg.Plugins.Popup.Contracts;
using DijiWalk.Mobile.ViewModels.ViewEntities;
using DijiWalk.Common.Contracts;
using DijiWalk.Common.StringExtension;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DijiWalk.Mobile
{
    public partial class App
    {
        public static ViewPlayer User;

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");

            #region Property configuration
            //Application.Current.Properties["url"] = "http://dijiwalktest.hopto.org/api/";
            Application.Current.Properties["url"] = "https://10.0.2.2:5001/api/";
            Application.Current.Properties["APIKey"] = "AIzaSyCDgp4RQYA4bzroTJM2ltv0ef6ceuqW254";
            Application.Current.Properties["SecretKey"] = "38b87a53f6ccec0d76a91a2640f626f03b04fce6128773b7c80235b2";
            #endregion

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #region Services
            containerRegistry.RegisterSingleton<IAdministratorService, AdministratorService>();
            containerRegistry.RegisterSingleton<IAnswerService, AnswerService>();
            containerRegistry.RegisterSingleton<IAuthentificationService, AuthentificationService>();
            containerRegistry.RegisterSingleton<IGameService, GameService>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IMissionService, MissionService>();
            containerRegistry.RegisterSingleton<IOrganizerService, OrganizerService>();
            containerRegistry.RegisterSingleton<IPlayerService, PlayerService>();
            containerRegistry.RegisterSingleton<IPlayService, PlayService>();
            containerRegistry.RegisterSingleton<IRouteService, RouteService>();
            containerRegistry.RegisterSingleton<IRouteStepService, RouteStepService>();
            containerRegistry.RegisterSingleton<IRouteTagService, RouteTagService>();
            containerRegistry.RegisterSingleton<IStepService, StepService>();
            containerRegistry.RegisterSingleton<ITagService, TagService>();
            containerRegistry.RegisterSingleton<ITeamAnswerService, TeamAnswerService>();
            containerRegistry.RegisterSingleton<ITeamPlayerService, TeamPlayerService>();
            containerRegistry.RegisterSingleton<ITeamRouteService, TeamRouteService>();
            containerRegistry.RegisterSingleton<ITeamService, TeamService>();
            containerRegistry.RegisterSingleton<ITransportService, TransportService>();
            containerRegistry.RegisterSingleton<ITrialService, TrialService>();
            containerRegistry.RegisterSingleton<ITypeService, TypeService>();
            #endregion

            #region Pages / Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<QuizzPage, QuizzPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<EtapePage, EtapePageViewModel>();
            containerRegistry.RegisterForNavigation<WaitingPage, WaitingPageViewModel>();
            containerRegistry.RegisterForNavigation<ValidationPage, ValidationPageViewModel>();
            containerRegistry.RegisterForNavigation<GamePage, GamePageViewModel>();
            #endregion

            #region common
            containerRegistry.RegisterSingleton<IStringExtension, StringExtension>();
            #endregion
        }
    }
}