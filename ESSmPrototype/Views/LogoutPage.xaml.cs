namespace ESSmPrototype.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();

        // clear navigation stack
        Shell.Current.Navigation.PopToRootAsync();
        // redirect to login
        Shell.Current.Navigation.PushAsync(new LoginPage());

        // stop idle timeout timer
        if (Application.Current is App app)
        {
            app.StopIdleTimer();
        }
    }
}