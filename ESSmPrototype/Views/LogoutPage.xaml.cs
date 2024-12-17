

using ESSmPrototype.Custom;

namespace ESSmPrototype.Views;

public partial class LogoutPage : ContentPage
{
    public LogoutPage()
    {
        InitializeComponent();

        OnLogout();
    }
    private static async void RedirectLoginPage()
    {
        // Clear the navigation stack
        await Shell.Current.Navigation.PopToRootAsync(false);

        // Redirect to login page
        if (Application.Current != null)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }

    private static async void OnLogout()
    {
        await BasicAuth.NotifyLogout();

        RedirectLoginPage();

        // stop idle timeout timer
        if (Application.Current is App app)
        {
            app.StopIdleTimer();
        }
    }
}
