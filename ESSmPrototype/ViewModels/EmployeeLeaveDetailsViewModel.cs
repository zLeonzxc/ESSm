using System.Diagnostics;
using System.Net.Http;

namespace ESSmPrototype.ViewModels;

public partial class EmployeeLeaveDetailsViewModel : INotifyPropertyChanged
{
    private LeaveRequest? _selectedLeaveRequest;
    private string? _selectedApprovalType;
    private bool _isApprovalOptionsVisible;
    private string? _message;
    private bool _isLoadingRequest = false;
    private bool _isApprovalTypeEmpty = true;
    private bool _isLeaveRequestsEmpty = true; // initial should be empty
    public ObservableCollection<LeaveRequest> LeaveRequests { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public EmployeeLeaveDetailsViewModel()
    {
        LeaveRequests = new ObservableCollection<LeaveRequest>();
        RetrieveLeaveRequests();

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
                OnPropertyChanged(nameof(ApprovalStatus));
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
    public string? ApprovalStatus
    {
        get => SelectedLeaveRequest?.ApprovalStatus;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.ApprovalStatus != value)
            {
                SelectedLeaveRequest.ApprovalStatus = value;
                OnPropertyChanged(nameof(ApprovalStatus));
                OnPropertyChanged(nameof(StatusImage));
            }
        }
    }
    public string? LeaveReason => SelectedLeaveRequest?.Reason;
    public string? Remarks
    {
        get => SelectedLeaveRequest?.Remarks;
        set
        {
            if (SelectedLeaveRequest != null && SelectedLeaveRequest.Remarks != value)
            {
                SelectedLeaveRequest.Remarks = value;
                OnPropertyChanged(nameof(Remarks));
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
            _isApprovalTypeEmpty = false;
            Debug.WriteLine($"SelectedApprovalType changed to: {SelectedApprovalType}");
            Debug.WriteLine($"ItemsSource: {(SelectedApprovalType == "Overtime" ? "OTRequests" : "LeaveRequests")}");
            Debug.WriteLine($"ItemTemplate: {(SelectedApprovalType == "Overtime" ? "OvertimeTemplate" : "LeaveTemplate")}");
            Debug.WriteLine($"ShowList changed to: {ShowList}");



            OnPropertyChanged(nameof(SelectedApprovalType));
            OnPropertyChanged(nameof(ShowList));
        }
    }

    public string StatusImage
    {
        get
        {
            return ApprovalStatus switch
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

    public bool ShowList
    {
        get => !_isApprovalTypeEmpty;
        set
        {
            if (_isApprovalTypeEmpty != value)
            {
                _isApprovalTypeEmpty = value;
                OnPropertyChanged(nameof(ShowList));
            }
        }
    }
    public string? Message
    {
        get => _message ?? string.Empty;
        set
        {
            if (_message != value)
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
    }

    public bool IsLoadingRequest
    {
        get => _isLoadingRequest;
        set
        {
            if (_isLoadingRequest != value)
            {
                _isLoadingRequest = value;
                OnPropertyChanged(nameof(IsLoadingRequest));
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
            ApprovalStatus = "Approved";
            var toast = Toast.Make("Request has been approved", ToastDuration.Short);
            await toast.Show();
        }
    }

    protected virtual async void OnRejectCommand()
    {
        if (SelectedLeaveRequest != null)
        {
            ApprovalStatus = "Rejected";
            var toast = Toast.Make("Request has been rejected", ToastDuration.Short);
            await toast.Show();
            if (Application.Current?.MainPage != null)
            {
                Remarks = await Application.Current.MainPage.DisplayPromptAsync("Comments", "");
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
        if (!_isLeaveRequestsEmpty)
        {
            return;
        }
        await RequestLeavesFromAPI();
    }

    private async Task RequestLeavesFromAPI()
    {
        IsLoadingRequest = true;

        if (_isLeaveRequestsEmpty)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var httpClient = new HttpClient(handler);

                var response = await httpClient.GetAsync("https://10.0.2.2:7087/api/LeaveRequests/");

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

            finally
            {
                IsLoadingRequest = false;
                _isLeaveRequestsEmpty = false;
            }
        }
        
    }
}
