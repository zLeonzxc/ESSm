namespace ESSmPrototype.ViewModels
{
    public partial class PendingApprovalViewModel : EmployeeLeaveDetailsViewModel
    {
        public ObservableCollection<OTRequest> OTRequests { get; set; }

        private OTRequest? _selectedOTRequest;
        private bool _isOTRequestEmpty = true; // Ensure this is initialized to true

        public OTRequest? SelectedOTRequest
        {
            get => _selectedOTRequest;
            set
            {
                if (_selectedOTRequest != value)
                {
                    _selectedOTRequest = value;
                    OnPropertyChanged(nameof(SelectedOTRequest));
                    OnPropertyChanged(nameof(EmployeeID));
                    OnPropertyChanged(nameof(EmployeeName));
                    OnPropertyChanged(nameof(EmployeeDep));
                    OnPropertyChanged(nameof(OTMonth));
                    OnPropertyChanged(nameof(TotalHours));
                    OnPropertyChanged(nameof(AppliedDate));
                    OnPropertyChanged(nameof(OTStartTime));
                    OnPropertyChanged(nameof(OTEndTime));
                }
            }
        }

        public new string EmployeeID => SelectedOTRequest?.EmployeeID ?? string.Empty;
        public new string EmployeeName => SelectedOTRequest?.LegalName ?? string.Empty;
        public new string EmployeeDep => SelectedOTRequest?.Department ?? string.Empty;
        public new DateTime AppliedDate => SelectedOTRequest?.AppliedDate ?? DateTime.Now;
        public string OTMonth => AppliedDate.ToString("MMMM yyyy");
        public double TotalHours => SelectedOTRequest?.TotalHours ?? 0;
        public TimeSpan OTStartTime => SelectedOTRequest?.OTStartTime ?? TimeSpan.Zero;
        public TimeSpan OTEndTime => SelectedOTRequest?.OTEndTime ?? TimeSpan.Zero;
        public string? OTReason => SelectedOTRequest?.Reason;
        public new string? Remarks
        {
            get => SelectedOTRequest?.Remarks;
            set
            {
                if (SelectedOTRequest != null && SelectedOTRequest.Remarks != value)
                {
                    SelectedOTRequest.Remarks = value;
                    OnPropertyChanged(nameof(Remarks));
                }
            }
        }
        public new string? ApprovalStatus
        {
            get => SelectedOTRequest?.ApprovalStatus;
            set
            {
                if (SelectedOTRequest != null && SelectedOTRequest.ApprovalStatus != value)
                {
                    SelectedOTRequest.ApprovalStatus = value;
                    OnPropertyChanged(nameof(ApprovalStatus));
                    OnPropertyChanged(nameof(StatusImage));
                }
            }
        }

        public PendingApprovalViewModel()
        {
            OTRequests = new ObservableCollection<OTRequest>();

            RetrieveOTRequests();
        }

        private async void RetrieveOTRequests()
        {
            if (!_isOTRequestEmpty)
            {
                return;
            }
            await LoadOTRequests();
        }

        private async Task LoadOTRequests()
        {
            IsLoadingRequest = true;

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var httpClient = new HttpClient(handler);

                var response = await httpClient.GetAsync("https://10.0.2.2:7087/api/OvertimeRequests/");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);

                    if (!string.IsNullOrEmpty(content))
                    {
                        // Make sure collection is empty
                        OTRequests.Clear();

                        // Parse the JSON response
                        using (JsonDocument document = JsonDocument.Parse(content))
                        {
                            JsonElement root = document.RootElement;
                            if (root.ValueKind == JsonValueKind.Array)
                            {
                                foreach (JsonElement element in root.EnumerateArray())
                                {
                                    var otRequest = new OTRequest
                                    {
                                        Id = element.GetProperty("id").GetInt32(),
                                        EmployeeID = element.GetProperty("employeeID").GetString(),
                                        LegalName = element.GetProperty("legalName").GetString(),
                                        Department = element.GetProperty("department").GetString(),
                                        Reason = element.GetProperty("reason").GetString(),
                                        Remarks = element.GetProperty("remarks").GetString(),
                                        AppliedDate = element.GetProperty("appliedDate").GetDateTime(),
                                        OTStartTime = TimeSpan.Parse(element.GetProperty("otStartTime").GetString() ?? "00:00:00"),
                                        OTEndTime = TimeSpan.Parse(element.GetProperty("otEndTime").GetString() ?? "00:00:00"),
                                        TotalHours = element.GetProperty("totalHours").GetDouble(),
                                        ApprovalStatus = element.GetProperty("approvalStatus").GetString()
                                    };

                                    OTRequests.Add(otRequest);
                                }
                            }
                        }
                    }
                    else
                    {
                        Message = "Failed to retrieve OT requests.";
                    }
                }
                else
                {
                    Message = $"API server error: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                Message = $"API server error: {ex.Message}";
            }
            finally
            {
                IsLoadingRequest = false;
                _isOTRequestEmpty = false;

                Debug.WriteLine($"OTRequests count: {OTRequests.Count}");
            }
        }
        protected override async void OnAcceptCommand()
        {
            if (SelectedOTRequest != null)
            {
                ApprovalStatus = "Approved";
                var toast = Toast.Make("Request has been approved", ToastDuration.Short);
                await toast.Show();
            }
        }

        protected override async void OnRejectCommand()
        {
            if (SelectedOTRequest != null)
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
    }
}
