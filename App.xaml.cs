namespace ESSmPrototype
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (App.Current != null)
            {
                App.Current.UserAppTheme = AppTheme.Light;
            }

            bool isLoggedIn = false;

            MainPage = isLoggedIn ? new AppShell() : new LoginPage();
        }
    }
}
