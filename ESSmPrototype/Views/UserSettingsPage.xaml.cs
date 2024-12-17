using ESSmPrototype.Custom;

namespace ESSmPrototype.Views;

public partial class UserSettingsPage : ContentPage
{
    public UserSettingsPage()
    {
        InitializeComponent();
    }

    private async void OnChangeComCodeClicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmation = await ShowPromptMessage();

            if (!confirmation)
            {
                return;
            }

            await BasicAuth.NotifyLogout();

            SecureStorage.Default.Remove("CompanyCode");
            SecureStorage.Default.Remove("Password");
            Preferences.Default.Remove("Username");
            Preferences.Default.Remove("RememberMe");
            Preferences.Default.Remove("AutoLogin");
            await Shell.Current.Navigation.PopToRootAsync(false);
            if (Application.Current != null)
            {
                Application.Current.MainPage = new NavigationPage(new StartedPage());
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    private static async Task<bool> ShowPromptMessage()
    {
        if (Application.Current?.MainPage != null)
        {
            bool confirmation = await Application.Current.MainPage.DisplayAlert("Warning", "Upon confirming you will be: \n• Signed out of the current session\n• Removing the saved user data \n  (username and password)", "Confirm", "Cancel");
            return confirmation;
        }

        return false;
    }
}
