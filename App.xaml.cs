namespace ESSmPrototype
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Force app theme to be in light mode
            if (App.Current != null)
            {
                App.Current.UserAppTheme = AppTheme.Light;
            }

            bool isLoggedIn = false;

            //MainPage = new AppFlyoutPage();

            MainPage = isLoggedIn ? new AppShell() : new LoginPage();
        }
    }
}
 
