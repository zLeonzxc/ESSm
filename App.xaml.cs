namespace ess_prototype
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isLoggedIn = false;

            MainPage = isLoggedIn ? new AppShell() : new LoginPage();
        }
    }
}
