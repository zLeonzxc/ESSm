using System.ComponentModel;
using System.Windows.Input;
using ESSmPrototype.Views.Employee;
using System.Collections.ObjectModel;

namespace ESSmPrototype.ViewModels
{
    public partial class EmployeePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //public ObservableCollection<MenuItem> MenuItems { get; }

    //public EmployeePageViewModel()
    //{
    //    MenuItems = new ObservableCollection<MenuItem>
    //        {
    //            new MenuItem { Title = "Menu Item 1", Command = new Command(() => NavigateTo(nameof(EmployeeMenuItem1))) },
    //            new MenuItem { Title = "Menu Item 2", Command = new Command(() => NavigateTo(nameof(EmployeeMenuItem2))) },
    //            new MenuItem { Title = "Menu Item 3", Command = new Command(() => NavigateTo(nameof(EmployeeMenuItem3))) }
    //        };
    //}

    //private async void NavigateTo(string route)
    //{
    //    await Shell.Current.GoToAsync(route);
    //}

    // Commands for navigation
    //    public ICommand NavigateToMenuItem1Command { get; }
    //    public ICommand NavigateToMenuItem2Command { get; }
    //    public ICommand NavigateToMenuItem3Command { get; }

    //    public EmployeePageViewModel()
    //    {
    //        // Initialize navigation commands with methods
    //        NavigateToMenuItem1Command = new Command(async () => await Shell.Current.GoToAsync(nameof(EmployeeMenuItem1Page)));
    //        NavigateToMenuItem2Command = new Command(async () => await Shell.Current.GoToAsync(nameof(EmployeeMenuItem2Page)));
    //        NavigateToMenuItem3Command = new Command(async () => await Shell.Current.GoToAsync(nameof(EmployeeMenuItem3Page)));
    //    }


}