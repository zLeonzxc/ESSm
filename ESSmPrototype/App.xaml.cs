using Timer = System.Timers.Timer;

namespace ESSmPrototype
{
    public partial class App : Application
    {
        private readonly Timer IdleTimer = new(10 * 60000); // 10 minutes
        string? companyCode;

        // FOR DEBUG ONLY
        //Timer LogTimer = new Timer(1000); // 1 second

        public App()
        {
            InitializeComponent();

            // Force app theme to be in light mode
            if (App.Current != null)
            {
                App.Current.UserAppTheme = AppTheme.Light;
            }
            
            var startedPageVM = new StartedPageViewModel();
            BindingContext = new LoginViewModel(startedPageVM.CompanyCode);

            MainPage = new NavigationPage(new StartedPage());
            SetMainPageAsync().ConfigureAwait(false);

            // FOR DEBUG ONLY
            //// Log Timer
            //LogTimer.Elapsed += LogRemainingTime;
            //LogTimer.Start();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            SetMainPageAsync().ConfigureAwait(false);
            return window;
        }

        private async Task SetMainPageAsync()
        {
            try
            {
                // Check if the app is being opened for the first time
                companyCode = await SecureStorage.GetAsync("CompanyCode");

                Debug.WriteLine(companyCode);

                if (string.IsNullOrEmpty(companyCode))
                {
                    MainPage = new NavigationPage(new StartedPage());
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            catch (Exception ex)
            {
                // Handle potential exceptions, such as when the device does not support secure storage
                Console.WriteLine($"Error retrieving CompanyCode from secure storage: {ex.Message}");
                MainPage = new NavigationPage(new StartedPage()); // Fallback to StartedPage in case of an error
            }
        }

        async void IdleCountdown_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await ShowSessionExpiredMessage();
                    await Shell.Current.Navigation.PopToRootAsync(false); // Clear the navigation stack
                    Application.Current!.MainPage = new LogoutPage();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured during session timeout: {ex.Message}");
            }
        }
        public void ResetIdleTimer()
        {
            IdleTimer.Stop();
            StartIdleTimer();
            Debug.WriteLine(":::Idle timer reset");
        }

        // FOR DEBUG ONLY
        //void LogRemainingTime(object? sender, ElapsedEventArgs e)
        //{
        //    var remainingTime = IdleTimer.Interval - (DateTime.Now - IdleTimerStartTime).TotalMilliseconds;
        //    Debug.WriteLine($":::Idle Timer Countdown - {remainingTime / 1000} seconds remaining");
        //}

        public void StartIdleTimer()
        {
            if (IdleTimer.Enabled)
            {
                IdleTimer.Stop();
            }

            IdleTimer.Elapsed -= IdleCountdown_Elapsed; // Unsubscribe to avoid multiple subscriptions
            IdleTimer.Elapsed += IdleCountdown_Elapsed;
            DateTime IdleTimerStartTime = DateTime.Now;
            IdleTimer.Start();
        }
        public void StopIdleTimer()
        {
            IdleTimer.Stop();
        }

        private static async Task ShowSessionExpiredMessage()
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Session Expired", "Session has been terminated. Please sign in again.", "OK");
            }
        }
    }
}
 
