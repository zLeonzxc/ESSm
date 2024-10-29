using ESSmPrototype.ViewModels;
using ESSmPrototype.Views;
using Microsoft.Maui.Controls;

namespace ESSmPrototype
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
