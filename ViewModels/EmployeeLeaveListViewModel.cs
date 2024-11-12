namespace ESSmPrototype.ViewModels
{
    public partial class EmployeeLeaveListViewModel : EmployeeLeaveDetailsViewModel
    {
        private ObservableCollection<LeaveRequest>? _filteredLeaveRequests;
        private string? _empID;
        private string? _empName;
        private string? _selectedMonth;
        private int? _selectedYear;
        private string? _selectedLeaveDescription;
        private string? _selectedLeaveStatus;
        private bool _isListVisible;

        public EmployeeLeaveListViewModel()
        {
            FilteredLeaveRequests = new ObservableCollection<LeaveRequest>(LeaveRequests);
            IsListVisible = false;
        }

        public ObservableCollection<LeaveRequest>? FilteredLeaveRequests
        {
            get => _filteredLeaveRequests;
            set
            {
                _filteredLeaveRequests = value;
                OnPropertyChanged(nameof(FilteredLeaveRequests));
            }
        }

        public string? EmpID
        {
            get => _empID;
            set
            {
                _empID = value;
                OnPropertyChanged(nameof(EmpID));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public string? EmpName
        {
            get => _empName;
            set
            {
                _empName = value;
                OnPropertyChanged(nameof(EmpName));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public string? SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public int? SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public string? SelectedLeaveDescription
        {
            get => _selectedLeaveDescription;
            set
            {
                _selectedLeaveDescription = value;
                OnPropertyChanged(nameof(SelectedLeaveDescription));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public string? SelectedLeaveStatus
        {
            get => _selectedLeaveStatus;
            set
            {
                _selectedLeaveStatus = value;
                OnPropertyChanged(nameof(SelectedLeaveStatus));
                _ = FilterLeaveRequestsAsync();
            }
        }

        public bool IsListVisible
        {
            get => _isListVisible;
            set
            {
                _isListVisible = value;
                OnPropertyChanged(nameof(IsListVisible));
            }
        }

        private async Task FilterLeaveRequestsAsync()
        {
            var filtered = await Task.Run(() =>
            {
                var tempFiltered = new ObservableCollection<LeaveRequest>();

                foreach (var lr in LeaveRequests)
                {
                    bool matchesEmpID = string.IsNullOrEmpty(EmpID) || (lr.EmployeeID != null && lr.EmployeeID.Contains(EmpID, StringComparison.OrdinalIgnoreCase));
                    bool matchesEmpName = string.IsNullOrEmpty(EmpName) || (lr.EmployeeName != null && lr.EmployeeName.Contains(EmpName, StringComparison.OrdinalIgnoreCase));
                    bool matchesSelectedMonth = string.IsNullOrEmpty(SelectedMonth) || lr.LeaveStartDate.ToString("MMMM") == SelectedMonth;
                    bool matchesSelectedYear = !SelectedYear.HasValue || lr.LeaveStartDate.Year == SelectedYear;
                    bool matchesLeaveDescription = string.IsNullOrEmpty(SelectedLeaveDescription) || lr.LeaveReason == SelectedLeaveDescription;
                    bool matchesLeaveStatus = string.IsNullOrEmpty(SelectedLeaveStatus) || lr.LeaveApprovalStatus == SelectedLeaveStatus;

                    if (matchesEmpID && matchesEmpName && matchesSelectedMonth && matchesSelectedYear && matchesLeaveDescription && matchesLeaveStatus)
                    {
                        tempFiltered.Add(lr);
                    }
                }

                return tempFiltered;
            });

            FilteredLeaveRequests = new ObservableCollection<LeaveRequest>(filtered);
            IsListVisible = FilteredLeaveRequests.Any();
        }
    }
}
