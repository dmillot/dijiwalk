﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Plugin.CurrentActivity;
using Plugin.Media;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Rg.Plugins.Popup;
using Sharpnado.Presentation.Forms.Droid;
using System.Net;
using System.Reflection;
using Xamarin.Forms;
using XamEffects.Droid;

[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]

namespace DijiWalk.Mobile.Droid
{
    [Activity(Label = "DijiWalk.Mobile", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;

            base.OnCreate(bundle);
            
            await CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            CachedImageRenderer.Init(enableFastRenderer: true);
            CachedImageRenderer.InitImageViewHandler();
            var ignore = typeof(SvgCachedImage);
            Popup.Init(this, bundle);
            Effects.Init();
            SharpnadoInitializer.Initialize(enableInternalLogger: true);
  
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

