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
}