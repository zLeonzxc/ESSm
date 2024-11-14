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

}
