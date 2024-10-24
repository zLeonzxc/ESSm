namespace ess_prototype.Views;

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
            Application.Current.MainPage = new NavigationPage(new AppShell());
        }
    }
}
