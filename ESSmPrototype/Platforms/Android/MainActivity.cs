using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ESSmPrototype.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MauiAppCompatActivity
    {
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            (App.Current as App)?.ResetIdleTimer();
        }
    }
}
