namespace ESSmPrototype.ViewModels;

public partial class EmployeeLeaveDetailsViewModel : INotifyPropertyChanged
{
    private LeaveRequest? _selectedLeaveRequest;
    private string? _selectedApprovalType;
    private bool _isApprovalOptionsVisible;
    public ObservableCollection<LeaveRequest> LeaveRequests { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public EmployeeLeaveDetailsViewModel()
    {
        LeaveRequests =
        [
            new("MY001", "John", "IT", "Pending", "Medical Leave", new DateTime(2023, 1, 1), new DateTime(2023, 1, 10), new DateTime(2023, 1, 15), ""),
            new("MY002", "Jane", "IT", "Pending", "Annual Leave", new DateTime(2023, 2, 1), new DateTime(2023, 2, 10), new DateTime(2023, 2, 15), ""),
            new("MY003", "Ben", "IT", "Pending", "Maternity Leave", new DateTime(2023, 3, 1), new DateTime(2023, 3, 10), new DateTime(2023, 3, 15), ""),
            new("MY004", "Henry", "IT", "Pending", "Paternity Leave", new DateTime(2023, 4, 1), new DateTime(2023, 4, 10), new DateTime(2023, 4, 15), ""),
            new("MY005", "Dave", "IT", "Pending", "Sick Leave", new DateTime(2023, 5, 1), new DateTime(2023, 5, 10), new DateTime(2023, 5, 15), ""),
            new("MY006", "Ali", "IT", "Pending", "Unpaid Leave", new DateTime(2023, 6, 1), new DateTime(2023, 6, 10), new DateTime(2023, 6, 15), ""),
            new("MY007", "Ahmad", "IT", "Pending", "Emergency Leave", new DateTime(2023, 7, 1), new DateTime(2023, 7, 10), new DateTime(2023, 7, 15), ""),
            new("MY008", "Siti", "IT", "Pending", "Marriage Leave", new DateTime(2023, 8, 1), new DateTime(2023, 8, 10), new DateTime(2023, 8, 15), ""),
            new("MY009", "Tamil", "IT", "Pending", "Compassionate Leave", new DateTime(2023, 9, 1), new DateTime(2023, 9, 10), new DateTime(2023, 9, 15), ""),
            new("MY010", "Raj", "IT", "Pending", "Study Leave", new DateTime(2023, 10, 1), new DateTime(2023, 10, 10), new DateTime(2023, 10, 15), "")
        ];

        AcceptCommand = new Command(OnAcceptCommand);
        RejectCommand = new Command(OnRejectCommand);
        NavigateToDetailsCommand = new Command<LeaveRequest>(OnNavigateToDetailsCommand);
    }

    public LeaveRequest? SelectedLeaveRequest
    {
        get => _selectedLeaveRequest;
        set
        {
            if (_selectedLeaveRequest != value)
            {
                _selectedLeaveRequest = value;
                OnPropertyChanged(nameof(SelectedLeaveRequest));
                OnPropertyChanged(nameof(EmployeeID));
                OnPropertyChanged(nameof(EmployeeName));
                OnPropertyChanged(nameof(EmployeeDep));
                OnPropertyChanged(nameof(LeaveApprovalStatus));
                OnPropertyChanged(nameof(LeaveReason));
                OnPropertyChanged(nameof(AppliedDate));
                OnPropertyChanged(nameof(LeaveStartDate));
                OnPropertyChanged(nameof(LeaveEndDate));
            }
        }
    }

    public string? EmployeeID => SelectedLeaveRequest?.EmployeeID;
    public string? EmployeeName => SelectedLeaveRequest?.EmployeeName;
    public string? EmployeeDep => SelectedLeaveRequest?.EmployeeDep;
    public string? LeaveApprovalStatus
    {
        get => SelectedLeaveRequest?.LeaveApprovalStatus;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.LeaveApprovalStatus != value)
            {
                SelectedLeaveRequest.LeaveApprovalStatus = value;
                OnPropertyChanged(nameof(LeaveApprovalStatus));
                OnPropertyChanged(nameof(StatusImage));
            }
        }
    }
    public string? LeaveReason => SelectedLeaveRequest?.LeaveReason;
    public string? LeaveComment
    {
        get => SelectedLeaveRequest?.LeaveComment;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.LeaveComment != value)
            {
                SelectedLeaveRequest.LeaveComment = value;
                OnPropertyChanged(nameof(LeaveComment));
            }
        }
    }
    public DateTime AppliedDate => SelectedLeaveRequest?.AppliedDate ?? default;
    public DateTime LeaveStartDate => SelectedLeaveRequest?.LeaveStartDate ?? default;
    public DateTime LeaveEndDate => SelectedLeaveRequest?.LeaveEndDate ?? default;

    public string? SelectedApprovalType
    {
        get => _selectedApprovalType;
        set
        {
            _selectedApprovalType = value;
            OnPropertyChanged(nameof(SelectedApprovalType));
        }
    }

    public string StatusImage
    {
        get
        {
            return LeaveApprovalStatus switch
            {
                "Approved" => "approvedleave.png",
                "Rejected" => "rejectedleave.png",
                "Pending" => "pending.png",
                _ => "form.png"
            };
        }
    }

    public bool IsApprovalOption
    {
        get => _isApprovalOptionsVisible;
        set
        {
            if (_isApprovalOptionsVisible != value)
            {
                _isApprovalOptionsVisible = value;
                OnPropertyChanged(nameof(IsApprovalOption));
            }
        }
    }
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand? AcceptCommand { get; set; }
    public ICommand? RejectCommand { get; set; }
    public ICommand NavigateToDetailsCommand { get; }

    protected virtual async void OnAcceptCommand()
    {
        if (SelectedLeaveRequest != null)
        {
            LeaveApprovalStatus = "Approved";
            var toast = Toast.Make("Leave request approved", ToastDuration.Short);
            await toast.Show();
        }
    }

    protected virtual async void OnRejectCommand()
    {
        if (SelectedLeaveRequest != null)
        {
            LeaveApprovalStatus = "Rejected";
            var toast = Toast.Make("Leave request rejected", ToastDuration.Short);
            await toast.Show();
            if (Application.Current?.MainPage != null)
            {
                LeaveComment = await Application.Current.MainPage.DisplayPromptAsync("Comments", "");
            }
        }
    }

    private async void OnNavigateToDetailsCommand(LeaveRequest leaveRequest)
    {
        try
        {
            SelectedLeaveRequest = leaveRequest;
            var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectedLeaveRequest", leaveRequest }
                };
            await Shell.Current.Navigation.PushAsync(new EmployeeLeaveDetailsPageView(leaveRequest));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation failed: {ex.Message}");
        }
    }
}
