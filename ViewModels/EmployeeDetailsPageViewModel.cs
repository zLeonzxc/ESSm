namespace ESSmPrototype.ViewModels
{
    public partial class EmployeeDetailsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee? _selectedEmployee;
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
        public string? Message { get; set;}

        public void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            int index = -1;

            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].EmployeeID == searchText || Employees[i].EmployeeName == searchText || Employees[i].EmployeeLegalName == searchText || Employees[i].EmployeeLoginID == searchText)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                // bind the selected employee to the view
                SelectedEmployee = Employees[index];
                Message = "";
            }
            else
            {
                // bind message to display error message
                Message = "Employee not found";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}