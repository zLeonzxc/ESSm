using System.ComponentModel;
using System.Windows.Input;

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

        private void OnLogin()
        {
            // Dummy authentication
            if (Username == "admin" && Password == "password")
            {
                if (Application.Current != null)
                {
                    Username = _username;
                    Application.Current.MainPage = new AppShell();
                }
                else
                    Message = "Uh oh... An unknown error has occured. \n[Error Code: ESSM1001]";
            }
            else
                Message = "Invalid username or password.\n[Error Code: ESSM1002]";
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
            LoginCommand = new Command(OnLogin);

            RetrieveName = new Command(GetName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
