namespace ESSmPrototype.ViewModels
{
    public class AppFlyoutPageViewModel
    {
        public ICommand NavigateCommand { get; }

        public AppFlyoutPageViewModel()
        {
            NavigateCommand = new Command<string>(async (pageName) => await Navigate(pageName));
        }

        private async Task Navigate(string pageName)
        {
            Page page = pageName switch
            {
                "MainPage" => new MainPage(),
                "EmployeePage" => new EmployeePage(),
                "ApproverPage" => new ApproverPage(),
                "HRAdminPage" => new HRAdminPage(),
                "LoginPage" => new LoginPage(),
                _ => new MainPage()
            };

            if (Application.Current?.MainPage is FlyoutPage flyoutPage)
            {
                // Perform navigation on the main thread
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    flyoutPage.Detail = new NavigationPage(page);
                    flyoutPage.IsPresented = false;
                });
            }
        }
    }
}
