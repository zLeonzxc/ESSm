namespace ESSmPrototype.ViewModels
{
    public partial class EmployeeDetailsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee? _selectedEmployee;
        private string? _message;
        private Employee? matchedEmployee = null;
        public EmployeeDetailsPageViewModel()
        {
            Employees =
                [
                    new("MY001", "John", "John Smith", "jsmith", "Malaysia"),
                    new("MY002", "Jane", "Jane Doe", "janedoe", "Malaysia"),
                    new("MY003", "Ben", "Benjamin Tan", "benjamin", "Malaysia"),
                    new("MY004", "Henry", "Henry Tan", "henrytan", "Malaysia"),
                    new("MY005", "Dave", "David Cross", "dcross", "Malaysia"),
                    new("MY006", "Ali", "Muhammad Ali", "muhammadali", "Malaysia"),
                    new("MY007", "Ahmad", "Daniel Ahmad", "danielahmad", "Malaysia"),
                    new("MY008", "Siti", "Siti Maisarah", "smaisarah", "Malaysia"),
                    new("MY009", "Tamil", "Tamil Murugan", "tmurugan", "Malaysia"),
                    new("MY010", "Raj", "Rajesh Sarveen ", "rajveen", "Malaysia")
                ];

            SearchCommand = new Command<string>(searchText => SearchEmployee(searchText));
            ResetCommand = new Command(ClearEntries);
        }

        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(EmployeeID));
                    OnPropertyChanged(nameof(EmployeeName));
                    OnPropertyChanged(nameof(EmployeeLegalName));
                    OnPropertyChanged(nameof(EmployeeLoginID));
                    OnPropertyChanged(nameof(Country));
                    OnPropertyChanged(nameof(Image));
                    OnPropertyChanged(nameof(Message));
                }
                
            }
        }

        public string? EmployeeID => SelectedEmployee?.EmployeeID;
        public string? EmployeeName => SelectedEmployee?.EmployeeName;
        public string? EmployeeLegalName => SelectedEmployee?.EmployeeLegalName;
        public string? EmployeeLoginID => SelectedEmployee?.EmployeeLoginID;
        public string? Country => SelectedEmployee?.Country;
        public static string? Image => "profile.png";
        public string? Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public virtual async void SearchEmployee(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Message = "Please enter your search query.";
                return;
            }
            else
            {
                Message = "Searching...";
                await Task.Delay(2000);
            }

            bool employeeFound = false;

            foreach (var employee in Employees)
            {
                if ((employee.EmployeeID?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (employee.EmployeeName?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    Message = "Employee Record found";
                    matchedEmployee = employee;
                    SelectedEmployee = matchedEmployee;
                    employeeFound = true;
                    break;
                }
            }

            if (!employeeFound)
            {
                Message = "Employee not found";
                SelectedEmployee = null;
            }
        }

        public void ClearEntries()
        {
            SelectedEmployee = null;
            Message = string.Empty;
            matchedEmployee = null;

            // Notify that all properties related to SelectedEmployee have changed
            OnPropertyChanged(nameof(SelectedEmployee));
            OnPropertyChanged(nameof(EmployeeID));
            OnPropertyChanged(nameof(EmployeeName));
            OnPropertyChanged(nameof(EmployeeLegalName));
            OnPropertyChanged(nameof(EmployeeLoginID));
            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(Message));
        }



        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}