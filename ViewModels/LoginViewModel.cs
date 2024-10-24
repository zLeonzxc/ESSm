using System.ComponentModel;
using System.Windows.Input;
using ess_prototype.Models;
using ess_prototype.Views;


namespace ess_prototype.ViewModels
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
                _username = value;
                OnPropertyChanged(nameof(Username));
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
        public ICommand LoginCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private void OnLogin()
        {
            // Dummy authentication
            if (Username == "admin" && Password == "password")
            {
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Message = "Application.Current is null.";
                }
            }
            else
            {
                Message = "Invalid username or password.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
