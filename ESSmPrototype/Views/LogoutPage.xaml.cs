

namespace ESSmPrototype.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();

        ClearSessionData();

        RedirectLoginPage();

        // stop idle timeout timer
        if (Application.Current is App app)
        {
            app.StopIdleTimer();
        }
    }

    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _companyCode = string.Empty;
    private bool _rememberMe = false;

    private void ClearSessionData()
    {
        // Retrieve values from Preferences and SecureStorage
        var username = Preferences.Get(nameof(Username), string.Empty);
        var password = SecureStorage.GetAsync(nameof(Password)).Result;
        var companyCode = SecureStorage.GetAsync(nameof(CompanyCode)).Result;
        var rememberMe = Preferences.Get(nameof(RememberMe), false);

        // Clear all data from Preferences and SecureStorage
        Preferences.Clear();
        SecureStorage.RemoveAll();

        // Reassign the stored values back to Preferences and SecureStorage
        Preferences.Set(nameof(Username), username);
        if (!string.IsNullOrEmpty(password))
        {
            SecureStorage.SetAsync(nameof(Password), password).Wait();
        }
        if (!string.IsNullOrEmpty(companyCode))
        {
            SecureStorage.SetAsync(nameof(CompanyCode), companyCode).Wait();
        }
        Preferences.Set(nameof(RememberMe), rememberMe);
    }

    private async void RedirectLoginPage()
    {
        // Clear the navigation stack
        await Shell.Current.Navigation.PopToRootAsync();

        // Redirect to login page
        if (Application.Current != null)
        {
            Application.Current.MainPage = new AppShell();
        }
        await Shell.Current.Navigation.PushAsync(new LoginPage());
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    private void SetProperty(ref string username, string value)
    {
        throw new NotImplementedException();
    }

    private void SetProperty(ref bool option, bool value)
    {
        throw new NotImplementedException();
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string CompanyCode
    {
        get => _companyCode;
        set => SetProperty(ref _companyCode, value);
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set => SetProperty(ref _rememberMe, value);
    }



}