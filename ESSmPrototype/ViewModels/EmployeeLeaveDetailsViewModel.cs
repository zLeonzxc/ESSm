using System.Net.Http;

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
        LeaveRequests = new ObservableCollection<LeaveRequest>();
        AcceptCommand = new Command(OnAcceptCommand);
        RejectCommand = new Command(OnRejectCommand);
        NavigateToDetailsCommand = new Command<LeaveRequest>(OnNavigateToDetailsCommand);
        RetrieveLeaveRequests();
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
    public string? EmployeeName => SelectedLeaveRequest?.LegalName;
    public string? EmployeeDep => SelectedLeaveRequest?.Department;
    public string? LeaveApprovalStatus
    {
        get => SelectedLeaveRequest?.ApprovalStatus;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.ApprovalStatus != value)
            {
                SelectedLeaveRequest.ApprovalStatus = value;
                OnPropertyChanged(nameof(LeaveApprovalStatus));
                OnPropertyChanged(nameof(StatusImage));
            }
        }
    }
    public string? LeaveReason => SelectedLeaveRequest?.Reason;
    public string? LeaveComment
    {
        get => SelectedLeaveRequest?.Remarks;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.Remarks != value)
            {
                SelectedLeaveRequest.Remarks = value;
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

    private async void RetrieveLeaveRequests()
    {
        await RequestLeavesFromAPI();
    }

    private async Task RequestLeavesFromAPI()
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var httpClient = new HttpClient(handler);

            var response = await httpClient.GetAsync("https://10.0.2.2:7087/api/LeaveRequests");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var leaveRequests = JsonSerializer.Deserialize<List<LeaveRequest>>(content);
                LeaveRequests = new ObservableCollection<LeaveRequest>(leaveRequests ?? new List<LeaveRequest>());

                Debug.WriteLine("Retrieved LeaveRequests:");
                foreach (var leaveRequest in LeaveRequests)
                {
                    Debug.WriteLine($"EmployeeID: {leaveRequest.EmployeeID}, EmployeeName: {leaveRequest.LegalName}, LeaveApprovalStatus: {leaveRequest.ApprovalStatus}, LeaveReason: {leaveRequest.Reason}, AppliedDate: {leaveRequest.AppliedDate}, LeaveStartDate: {leaveRequest.LeaveStartDate}, LeaveEndDate: {leaveRequest.LeaveEndDate}");
                }
            }
            else
            {
                Debug.WriteLine("Failed to retrieve leave requests: {0}", response);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to retrieve leave requests: {ex.Message}");
        }
    }
}
