using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Flex;
using Foundation;
using Prism;
using Prism.Ioc;
using Sharpnado.Presentation.Forms.iOS;
using System.Net;
using System.Reflection;
using UIKit;


namespace DijiWalk.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            var ignore = typeof(SvgCachedImage);
            FlexButton.Init();
            SharpnadoInitializer.Initialize(enableInternalLogger: true);
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
