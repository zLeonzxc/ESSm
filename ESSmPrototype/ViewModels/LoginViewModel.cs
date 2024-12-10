namespace ESSmPrototype.ViewModels
{
    public partial class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _rememberMe = false;
        private string _message = string.Empty;

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
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        //private void OnLogin()
        //{
        //    // Dummy authentication
        //    if (Username == "admin" && Password == "password")
        //    {
        //        if (Application.Current != null)
        //        {
        //            Username = _username;
        //            Application.Current.MainPage = new AppShell();
        //        }
        //        else
        //            Message = "Uh oh... An unknown error has occured. \n[Error Code: ESSM1001]";
        //    }
        //    else
        //        Message = "Invalid username or password.\n[Error Code: ESSM1002]";
        //}

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

                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Raw Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    if (Application.Current != null)
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        Message = "Uh oh... An unknown error has occurred. \n[Error Code: ESSM1001]";
                    }
                }
                else if (response.Content.Equals("ESSM1002"))
                {
                    Message = "User does not exist.\n[Error Code: ESSM1002]";
                }
                else if (response.Content.Equals("ESSM1003"))
                {
                    Message = "Invalid username or password.\n[Error Code: ESSM1003]";
                }
                else
                {
                    Message = $"An unknown error has occurred";
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
            LoginCommand = new Command(async() => await OnLogin());

            RetrieveName = new Command(GetName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
