namespace ESSmPrototype.Custom;

public partial class BottomNavBar : ContentView
{
    public BottomNavBar()
    {
        InitializeComponent();
    }

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            // Log or display the exception
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    private async void OnApprovalButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.Navigation.PushAsync(new PendingApprovalPage());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.Navigation.PushAsync(new UserSettingsPage());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }

}
