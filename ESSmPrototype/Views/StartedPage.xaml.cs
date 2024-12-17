namespace ESSmPrototype.Views;

public partial class StartedPage : ContentPage
{
    public string CompanyCode { get; set; }
    public StartedPage()
    {
        InitializeComponent();
        BindingContext = this;

        if (App.Current is App app)
        {
            app.StopIdleTimer();
        }
    }

    private async void OnSelectButtonClicked(object sender, EventArgs e)
    {
        // Save the selection as "completed" in SecureStorage
        await SecureStorage.SetAsync("CompanyCode", CompanyCode);

        // verify company code
        Debug.WriteLine($"Company code: {CompanyCode}");
        // Navigate to the LoginPage
        await Navigation.PushAsync(new LoginPage());

        // Optionally remove this page from the navigation stack
        Navigation.RemovePage(this);
    }
}
