namespace ESSmPrototype.Views;

public partial class StartedPage : ContentPage
{
    private StartedPageViewModel _vm;
    public StartedPage()
    {
        InitializeComponent();
        _vm = new StartedPageViewModel();
        BindingContext = _vm;

        if (App.Current is App app)
        {
            app.StopIdleTimer();
        }
    }

    private async void OnSelectButtonClicked(object sender, EventArgs e)
    {
        await SecureStorage.SetAsync("CompanyCode", _vm.CompanyCode);

        _vm.VerifyCompanyCode(_vm.CompanyCode);


        Debug.WriteLine("Navigated from StartedPage.xaml.cs");
        await Navigation.PushAsync(new LoginPage());

        Navigation.RemovePage(this);
    }
}
