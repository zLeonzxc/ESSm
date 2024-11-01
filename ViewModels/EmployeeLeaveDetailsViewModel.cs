namespace ESSmPrototype.ViewModels;

public class EmployeeLeaveDetailsViewModel : INotifyPropertyChanged
{
    private string? employeeID;
    private string? employeeName;
    private string? employeePosition;
    private string? leaveApprovalStatus;
    private string? leaveReason;

    public event PropertyChangedEventHandler? PropertyChanged;
	public EmployeeLeaveDetailsViewModel(string employeeID = "", 
                                         string employeeName = "", 
                                         string employeePosition = "",
                                         string leaveApprovalStatus = "", 
                                         string leaveReason = "")
	{
        EmployeeID = employeeID;
        EmployeeName = employeeName;
        EmployeePosition = employeePosition;
        LeaveApprovalStatus = leaveApprovalStatus;
        LeaveReason = leaveReason;
        AcceptCommand = new Command(OnAcceptCommand);
        RejectCommand = new Command(OnRejectCommand);
    }

    public EmployeeLeaveDetailsViewModel() : this("", "", "", "", "") { }

    public void OnPropertyChanged(string propertyName)
	{
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

	public string? EmployeeID
    {
        get => employeeID;
        set
        {
            if (employeeID != value)
            {
                employeeID = value;
                OnPropertyChanged(nameof(EmployeeID));
            }
        }
    }

    public string? EmployeeName
    {
        get => employeeName;
        set
        {
            if(employeeName != value)
            {
                employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }
    }
    
    public string? EmployeePosition
    {
        get => employeePosition;
        set
        {
            if (employeePosition != value)
            {
                employeePosition = value;
                OnPropertyChanged(nameof(EmployeePosition));
            }
        }
    }

    public string? LeaveApprovalStatus
    {
        get => leaveApprovalStatus;
        set
        {
            if (leaveApprovalStatus != value)
            {
                leaveApprovalStatus = value;
                OnPropertyChanged(nameof(LeaveApprovalStatus));
            }
        }
    }

    public string? LeaveReason
    {
        get => leaveReason;
        set
        {
            if (leaveReason != value)
            {
                leaveReason = value;
                OnPropertyChanged(nameof(LeaveReason));
            }
        }
    }

    private void ChangeLeaveStatus()
    {
        LeaveApprovalStatus = LeaveApprovalStatus == "Pending" ? "Approved" : "Pending";
    }

    public ICommand AcceptCommand { get; set; }
    public ICommand RejectCommand { get; set; }

    protected virtual void OnAcceptCommand() { }
    protected virtual void OnRejectCommand() { }
}