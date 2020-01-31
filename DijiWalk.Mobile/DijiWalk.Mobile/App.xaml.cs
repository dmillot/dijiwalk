using Prism;
using Prism.Ioc;
using DijiWalk.Mobile.ViewModels;
using DijiWalk.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DijiWalk.Mobile.Services.Interfaces;
using DijiWalk.Mobile.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DijiWalk.Mobile
{
    public partial class App
    {
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

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterSingleton<IGameService, GameService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<EtapePage, EtapePageViewModel>();
            containerRegistry.RegisterForNavigation<WaitingPage, WaitingPageViewModel>();
            containerRegistry.RegisterForNavigation<ValidationPage, ValidationPageViewModel>();
            containerRegistry.RegisterForNavigation<GamePage, GamePageViewModel>();
        }
    }
}
