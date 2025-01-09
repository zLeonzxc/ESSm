namespace ESSmPrototype.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    public LoginPage()
    {
        InitializeComponent();

        _viewModel = new LoginViewModel();
        BindingContext = _viewModel;

        if (App.Current is App app)
        {
            app.StopIdleTimer();
        }

        AutoLoginVerify(_viewModel);

    }

    private async void AutoLoginVerify(LoginViewModel _viewModel)
    {
        if (_viewModel.AutoLogin)
        {
            await _viewModel.AutoLoginUser();
        }
    }
}
