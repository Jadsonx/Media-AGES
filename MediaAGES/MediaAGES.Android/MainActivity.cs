using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Ads;

namespace MediaAGES.Droid
{
    [Activity(Label = "MediaAGES", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //Banner ADS Menu Inicial
            MobileAds.Initialize(ApplicationContext, "ca-app-pub-3659475632008000/9740724198");
            //Banner ADS Teorica
            //MobileAds.Initialize(ApplicationContext, "ca-app-pub-3659475632008000/7960166481");
            ////Banner ADS Prática
            //MobileAds.Initialize(ApplicationContext, "ca-app-pub-3659475632008000/3995928793");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

