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
        if (Application.Current != null)
        {
            //if (Application.Current is App app)
            //{
            //    app.StartIdleTimer();
            //}

            Application.Current.MainPage = new NavigationPage(new AppShell());
        }
    }
}
