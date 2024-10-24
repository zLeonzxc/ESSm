using ess_prototype.ViewModels;
using ess_prototype.Models;
using ess_prototype.Views;

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
