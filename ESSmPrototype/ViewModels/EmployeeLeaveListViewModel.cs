﻿namespace ESSmPrototype.ViewModels
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
        private bool _isSearchBoxVisible;

        public EmployeeLeaveListViewModel()
        {
            FilteredLeaveRequests = new ObservableCollection<LeaveRequest>(LeaveRequests);
            IsListVisible = false;
            IsSearchBoxVisible = true;
            Message = "";
            SearchFilter = new Command(async () => await FilterLeaveRequestsAsync());
            ResetFilter = new Command(ResetFilterEntries);
            ToggleSearchBoxCommand = new Command(ToggleSearchBox);

            //RetrieveLeaveRequests();

            // Initialize the ItemsSource properties
            Months =
            [
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            ];

            Years = [2022, 2023, 2024, 2025];

            LeaveDescriptions =
            [
                "Annual Leave", "Army Reserve", "Compassionate Leave / Bereavement Leave",
                "Jury Service", "Leave Without Pay", "Long Service Leave", "Maternity Leave",
                "Paternity Leave", "Personal Leave", "Personal Leave Without Pay",
                "Salary Continuance", "Special Leave", "Study Leave", "Volunteer Leave",
                "Workers Compensation"
            ];

            LeaveStatuses =
            [
                "Approved", "Cancelled", "Pending", "Rejected", "Saved",
                "Cancelled (Passed Monthly Cutoff)"
            ];
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
            }
        }

        public string? EmpName
        {
            get => _empName;
            set
            {
                _empName = value;
                OnPropertyChanged(nameof(EmpName));
            }
        }

        public string? SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
            }
        }

        public int? SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }

        public string? SelectedLeaveDescription
        {
            get => _selectedLeaveDescription;
            set
            {
                _selectedLeaveDescription = value;
                OnPropertyChanged(nameof(SelectedLeaveDescription));
            }
        }

        public string? SelectedLeaveStatus
        {
            get => _selectedLeaveStatus;
            set
            {
                _selectedLeaveStatus = value;
                OnPropertyChanged(nameof(SelectedLeaveStatus));
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

        public bool IsSearchBoxVisible
        {
            get => _isSearchBoxVisible;
            set
            {
                if (_isSearchBoxVisible != value)
                {
                    _isSearchBoxVisible = value;
                    OnPropertyChanged(nameof(IsSearchBoxVisible));
                }
            }
        }

        //public string? Message
        //{
        //    get => _message ?? string.Empty;
        //    set
        //    {
        //        if (_message != value)
        //        {
        //            _message = value;
        //            OnPropertyChanged(nameof(Message));
        //        }
        //    }
        //}

        // ItemsSource properties for the Pickers
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }
        public ObservableCollection<string> LeaveDescriptions { get; set; }
        public ObservableCollection<string> LeaveStatuses { get; set; }

        // Command to toggle the visibility of the search box
        public ICommand ToggleSearchBoxCommand { get; }

        private void ToggleSearchBox()
        {
            if (!IsSearchBoxVisible)
            {
                IsSearchBoxVisible = true;
                IsListVisible = false;
            }
        }

        private async Task FilterLeaveRequestsAsync()
        {
            // Clear message
            Message = "";

            IsSearchBoxVisible = false;

            var filtered = await Task.Run(() =>
            {
                var tempFiltered = new ObservableCollection<LeaveRequest>();

                foreach (var lr in LeaveRequests)
                {
                    bool matchesEmpID = string.IsNullOrEmpty(EmpID) || (lr.EmployeeID != null && lr.EmployeeID.Contains(EmpID, StringComparison.OrdinalIgnoreCase));
                    bool matchesEmpName = string.IsNullOrEmpty(EmpName) || (lr.LegalName != null && lr.LegalName.Contains(EmpName, StringComparison.OrdinalIgnoreCase));
                    bool matchesSelectedMonth = string.IsNullOrEmpty(SelectedMonth) || lr.LeaveStartDate.ToString("MMMM") == SelectedMonth;
                    bool matchesSelectedYear = !SelectedYear.HasValue || lr.LeaveStartDate.Year == SelectedYear;
                    bool matchesLeaveDescription = string.IsNullOrEmpty(SelectedLeaveDescription) || lr.Reason == SelectedLeaveDescription;
                    bool matchesLeaveStatus = string.IsNullOrEmpty(SelectedLeaveStatus) || lr.ApprovalStatus == SelectedLeaveStatus;

                    if (matchesEmpID && matchesEmpName && matchesSelectedMonth && matchesSelectedYear && matchesLeaveDescription && matchesLeaveStatus)
                    {
                        tempFiltered.Add(lr);
                    }
                }

                return tempFiltered;
            });

            if (string.IsNullOrEmpty(EmpID) && string.IsNullOrEmpty(EmpName) && string.IsNullOrEmpty(SelectedMonth) && !SelectedYear.HasValue && string.IsNullOrEmpty(SelectedLeaveDescription) && string.IsNullOrEmpty(SelectedLeaveStatus))
            {
                // Validation for user not entering any input (list will add every element)
                Message = "No records found.";
            }
            else if (filtered.Count == 0)
            {
                // Validation when list is empty
                Message = "No records found.";
            }
            else
            {
                FilteredLeaveRequests = new ObservableCollection<LeaveRequest>(filtered);
                IsListVisible = FilteredLeaveRequests.Any();
                OnPropertyChanged(nameof(IsListVisible));
            }
        }

        private void ResetFilterEntries()
        {
            EmpID = null;
            EmpName = null;
            SelectedMonth = null;
            SelectedYear = null;
            SelectedLeaveDescription = null;
            SelectedLeaveStatus = null;
            Message = "";

            FilteredLeaveRequests = new ObservableCollection<LeaveRequest>(LeaveRequests);
            IsListVisible = false;
            OnPropertyChanged(nameof(IsListVisible));
        }

        private async void RetrieveLeaveRequests()
        {
            if (LeaveRequests.Count != 0)
            {
                return;
            }
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var httpClient = new HttpClient(handler);

            try
            {
                var response = await httpClient.GetAsync("https://10.0.2.2:8198/api/LeaveRequests/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);

                    if (!string.IsNullOrEmpty(content))
                    {
                        // Clear the existing collection
                        LeaveRequests.Clear();

                        // Parse the JSON response
                        using (JsonDocument document = JsonDocument.Parse(content))
                        {
                            JsonElement root = document.RootElement;
                            if (root.ValueKind == JsonValueKind.Array)
                            {
                                foreach (JsonElement element in root.EnumerateArray())
                                {
                                    var leaveRequest = new LeaveRequest
                                    {
                                        Id = element.GetProperty("id").GetInt32(),
                                        EmployeeID = element.GetProperty("employeeID").GetString(),
                                        LegalName = element.GetProperty("legalName").GetString(),
                                        Department = element.GetProperty("department").GetString(),
                                        ApprovalStatus = element.GetProperty("approvalStatus").GetString(),
                                        Reason = element.GetProperty("reason").GetString(),
                                        Remarks = element.GetProperty("remarks").GetString(),
                                        AppliedDate = element.GetProperty("appliedDate").GetDateTime(),
                                        LeaveStartDate = element.GetProperty("leaveStartDate").GetDateTime(),
                                        LeaveEndDate = element.GetProperty("leaveEndDate").GetDateTime()
                                    };

                                    LeaveRequests.Add(leaveRequest);
                                }
                            }
                        }

                        // Log the content of the LeaveRequests collection
                        Debug.WriteLine("Retrieved LeaveRequests: {0}", LeaveRequests.Count());

                        foreach (var lr in LeaveRequests)
                        {
                            Debug.WriteLine($"ID: {lr.Id}, EmployeeID: {lr.EmployeeID}, LegalName: {lr.LegalName}, Department: {lr.Department}, ApprovalStatus: {lr.ApprovalStatus}, Reason: {lr.Reason}, Remarks: {lr.Remarks}, AppliedDate: {lr.AppliedDate}, LeaveStartDate: {lr.LeaveStartDate}, LeaveEndDate: {lr.LeaveEndDate}");
                        }
                    }
                    else
                    {
                        Message = "Failed to retrieve leave requests.";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"API server error: {ex.Message}";
            }
        }

        public ICommand SearchFilter { get; set; }
        public ICommand ResetFilter { get; set; }
    }
}

