namespace ESSmPrototype.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }


    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            if (Application.Current is App app)
            {
                app.StartIdleTimer();
            }
            Application.Current.MainPage = new NavigationPage(new AppShell());
        }
    }
}
