using System.ComponentModel;
using System.Windows.Input;
using ESSmPrototype.Views;
using ESSmPrototype.Models;

namespace ESSmPrototype.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand LogoutCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BaseViewModel()
        {
            LogoutCommand = new Command(OnLogout);
        }

        private void OnLogout()
        {
            if (Application.Current?.MainPage != null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private User _username;
        public User Username
        {
            get => _username;

        }
    }
}
