namespace ESSmPrototype.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.Current is App app)
        {
            app.StopIdleTimer();
        }

        if (BindingContext is LoginViewModel viewModel)
        {
            await viewModel.AutoLoginUser();
        }
    }


    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is LoginViewModel viewModel)
        {
            // var username = viewModel.Username;

            if (Application.Current != null)
            {
                // Pass the username to the AppShell constructor
                Application.Current.MainPage = new NavigationPage(new AppShell(viewModel));
            }
        }
    }
}
