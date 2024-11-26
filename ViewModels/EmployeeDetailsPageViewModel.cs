using Android.Widget;

namespace ESSmPrototype.ViewModels
{
    public partial class EmployeeDetailsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee? _selectedEmployee;
        private string? _message;
        private bool _isListVisible;
        private string? _employeeName;
        private ObservableCollection<Employee>? _filteredSearchResults { get; set; }
        public EmployeeDetailsPageViewModel()
        {
            Employees = new ObservableCollection<Employee>
            {
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
            };

            SearchCommand = new Command(async() => await SearchEmployee());
            ResetCommand = new Command(ClearEntries);
            EntryTap = new Command<Employee>(OnNavigateToDetailsCommand);
        }

        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    EmployeeName = _selectedEmployee?.EmployeeName;
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

        public ObservableCollection<Employee>? FilteredSearchResults
        {
            get => _filteredSearchResults;
            set
            {
                _filteredSearchResults = value;
                OnPropertyChanged(nameof(FilteredSearchResults));
            }
        }

        public bool IsListVisible
        {
            get => _isListVisible;
            set
            {
                if (_isListVisible != value)
                {
                    _isListVisible = value;
                    OnPropertyChanged(nameof(IsListVisible));
                }
            }
        }

        public string? EmployeeID => SelectedEmployee?.EmployeeID;
        public string? EmployeeName 
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(_employeeName));
            }
        }
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

        private async Task SearchEmployee()
        {
            var searchText = EmployeeName;
            bool employeeFound = false;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Message = "Please enter your search query.";
                return;
            }
            else
            {
                Message = "Searching...";
            }

            var filtered = await Task.Run(() =>
            {
                var tempFiltered = new ObservableCollection<Employee>();

                foreach (var emp in Employees)
                {
                    bool matchesEmpName = !string.IsNullOrEmpty(EmployeeName) && emp.EmployeeName != null && emp.EmployeeName.Contains(searchText, StringComparison.OrdinalIgnoreCase);

                    if (matchesEmpName)
                    {
                        employeeFound = true;
                        Message = "Employee record found";
                        tempFiltered.Add(emp);
                    }
                }

                return tempFiltered;
            });

            if (!employeeFound)
            {
                Message = "Employee not found";
                SelectedEmployee = null;
            }
            else
            {
                FilteredSearchResults = new ObservableCollection<Employee>(filtered);
                IsListVisible = FilteredSearchResults.Any();
                OnPropertyChanged(nameof(IsListVisible));
            }
        }

        public void ClearEntries()
        {
            SelectedEmployee = null;
            Message = string.Empty;
            FilteredSearchResults = null;
            IsListVisible = false;
            EmployeeName = string.Empty;

            // Notify that all properties related to SelectedEmployee have changed
            OnPropertyChanged(nameof(SelectedEmployee));
            OnPropertyChanged(nameof(EmployeeID));
            OnPropertyChanged(nameof(EmployeeName));
            OnPropertyChanged(nameof(EmployeeLegalName));
            OnPropertyChanged(nameof(EmployeeLoginID));
            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(IsListVisible));
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand EntryTap { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void OnNavigateToDetailsCommand(Employee employee)
        {
            try
            {
                SelectedEmployee = employee;
                OnPropertyChanged(nameof(SelectedEmployee));

                var tabbedPage = new NavigationPage(new EmployeeDetailsPageTab(employee));

                if (Application.Current?.MainPage?.Navigation != null)
                { 
                    Application.Current.MainPage = tabbedPage;
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation failed: {ex.Message}");
            }
        }
    }
}
