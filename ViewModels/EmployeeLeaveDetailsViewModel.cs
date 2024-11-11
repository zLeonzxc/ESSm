namespace ESSmPrototype.ViewModels;

public partial class EmployeeLeaveDetailsViewModel : INotifyPropertyChanged
{
    private LeaveRequest? _selectedLeaveRequest;
    private string? _selectedApprovalType;
    public ObservableCollection<LeaveRequest> LeaveRequests { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public EmployeeLeaveDetailsViewModel()
    {
        LeaveRequests =
        [
            new("MY001", "John", "Software Engineer", "Pending", "Medical Leave", new DateTime(2023, 1, 1), new DateTime(2023, 1, 10), new DateTime(2023, 1, 15)),
            new("MY002", "Jane", "Software Engineer", "Approved", "Annual Leave", new DateTime(2023, 2, 1), new DateTime(2023, 2, 10), new DateTime(2023, 2, 15)),
            new("MY003", "Ben", "Software Engineer", "Rejected", "Maternity Leave", new DateTime(2023, 3, 1), new DateTime(2023, 3, 10), new DateTime(2023, 3, 15)),
            new("MY004", "Henry", "Software Engineer", "Pending", "Paternity Leave", new DateTime(2023, 4, 1), new DateTime(2023, 4, 10), new DateTime(2023, 4, 15)),
            new("MY005", "Dave", "Software Engineer", "Pending", "Sick Leave", new DateTime(2023, 5, 1), new DateTime(2023, 5, 10), new DateTime(2023, 5, 15)),
            new("MY006", "Ali", "Software Engineer", "Pending", "Unpaid Leave", new DateTime(2023, 6, 1), new DateTime(2023, 6, 10), new DateTime(2023, 6, 15)),
            new("MY007", "Ahmad", "Software Engineer", "Pending", "Emergency Leave", new DateTime(2023, 7, 1), new DateTime(2023, 7, 10), new DateTime(2023, 7, 15)),
            new("MY008", "Siti", "Software Engineer", "Pending", "Marriage Leave", new DateTime(2023, 8, 1), new DateTime(2023, 8, 10), new DateTime(2023, 8, 15)),
            new("MY009", "Tamil", "Software Engineer", "Pending", "Compassionate Leave", new DateTime(2023, 9, 1), new DateTime(2023, 9, 10), new DateTime(2023, 9, 15)),
            new("MY010", "Raj", "Software Engineer", "Pending", "Study Leave", new DateTime(2023, 10, 1), new DateTime(2023, 10, 10), new DateTime(2023, 10, 15))
        ];

        AcceptCommand = new Command(OnAcceptCommand);
        RejectCommand = new Command(OnRejectCommand);
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
                OnPropertyChanged(nameof(EmployeePosition));
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
    public string? EmployeePosition => SelectedLeaveRequest?.EmployeePosition;
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
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand? AcceptCommand { get; set; }
    public ICommand? RejectCommand { get; set; }

    protected virtual void OnAcceptCommand()
    {
        if (SelectedLeaveRequest != null)
        {
            LeaveApprovalStatus = "Approved";
        }
    }

    protected virtual void OnRejectCommand()
    {
        if (SelectedLeaveRequest != null)
        {
            LeaveApprovalStatus = "Rejected";
        }
    }
}
