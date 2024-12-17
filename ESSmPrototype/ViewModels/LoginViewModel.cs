namespace ESSmPrototype.ViewModels
{
    public partial class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _rememberMe = false;
        private string _message = string.Empty;
        private bool _autoLogin = false;
        private bool _isErrorVisible = false;

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                if (_rememberMe != value)
                {
                    _rememberMe = value;
                    OnPropertyChanged(nameof(RememberMe));
                }
            }
        }

        public bool AutoLogin
        {
            get => _autoLogin;
            set
            {
                if (_autoLogin != value)
                {
                    _autoLogin = value;
                    OnPropertyChanged(nameof(AutoLogin));
                    Preferences.Set(nameof(AutoLogin), AutoLogin);
                }
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                    IsErrorVisible = !string.IsNullOrEmpty(_message);
                }
            }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                if (_isErrorVisible != value)
                {
                    _isErrorVisible = value;
                    OnPropertyChanged(nameof(IsErrorVisible));
                }
            }
        }

        private async Task OnLogin()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var httpClient = new HttpClient(handler);

            var loginDTO = new
            {
                username = Username,
                password = Password
            };
            var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("https://10.0.2.2:7087/api/Users/login", content);



                if (response.IsSuccessStatusCode)
                {
                    if (Application.Current != null)
                    {
                        if (RememberMe)
                        {
                            Preferences.Set(nameof(Username), Username);
                            Preferences.Set(nameof(RememberMe), RememberMe);
                            await SecureStorage.SetAsync(nameof(Password), Password);
                        }
                        else
                        {
                            Preferences.Set(nameof(RememberMe), RememberMe);
                        }

                        // Check if the current MainPage is already set to avoid re-adding it
                        if (Application.Current.MainPage is not AppShell)
                        {
                            Application.Current.MainPage = new AppShell(this);
                        }
                    }
                    else
                    {
                        Message = "Uh oh... An unknown error has occurred. \n[Error Code: ESSM1001]"; // maui app error
                    }
                }
                else
                {
                    Message = "Invalid username or password.\n[Error Code: ESSM1002]"; // wrong username or password
                }
            }
            catch (Exception ex)
            {
                //Message = $"An error occurred: {ex.Message}";
                Message = "Server unreachable. Please try again later.\n[Error Code:ESSM1003]"; // api server error
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoadStoredCredentials()
        {
            RememberMe = Preferences.Get(nameof(RememberMe), false);
            if (RememberMe)
            {
                Username = Preferences.Get(nameof(Username), string.Empty);
                Password = await SecureStorage.GetAsync(nameof(Password)) ?? string.Empty;
            }
        }

        public async Task AutoLoginUser()
        {
            try
            {
                AutoLogin = Preferences.Get(nameof(AutoLogin), false);
                RememberMe = Preferences.Get(nameof(RememberMe), false);

                if (RememberMe && AutoLogin)
                {
                    await LoadStoredCredentials();
                    await OnLogin();
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occurred: {ex.Message}";
            }
        }

        private void GetName()
        {
            var LoginViewModel = new LoginViewModel();
            Username = LoginViewModel.Username;
        }

        public ICommand LoginCommand { get; }
        public ICommand RetrieveName { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await OnLogin());
            RetrieveName = new Command(GetName);
            _autoLogin = Preferences.Get(nameof(AutoLogin), false);

            // Load stored credentials and attempt auto-login when the view model is created
            Task.Run(async () => await LoadStoredCredentials());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



