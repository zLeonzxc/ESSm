namespace ESSmPrototype.Models
{
    public class OTRequest : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string? EmployeeID { get; set; }
        public string? LegalName { get; set; }
        public string? Department { get; set; }
        public string? ApprovalStatus
        {
            get => _approvalStatus;
            set
            {
                if (_approvalStatus != value)
                {
                    _approvalStatus = value;
                    OnPropertyChanged(nameof(ApprovalStatus));
                    OnPropertyChanged(nameof(StatusImage));
                }
            }
        }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public DateTime AppliedDate { get; set; }
        public TimeSpan OTStartTime { get; set; }
        public TimeSpan OTEndTime { get; set; }
        public double TotalHours { get; set; }

        public OTRequest(string employeeID, string legalName, string department, string approvalStatus, string reason, DateTime appliedDate, TimeSpan otStartTime, TimeSpan otEndTime, double totalHours, string remarks)
        {
            EmployeeID = employeeID;
            LegalName = legalName;
            Department = department;
            ApprovalStatus = approvalStatus;
            Reason = reason;
            AppliedDate = appliedDate;
            OTStartTime = otStartTime;
            OTEndTime = otEndTime;
            TotalHours = totalHours;
            Remarks = remarks;
        }

        // Parameterless constructor for deserialization
        public OTRequest() { }

        // Constructor with parameters matching property names
        public OTRequest(int id, string? employeeID, string? legalName, string? department, string? reason, string? remarks, DateTime appliedDate, TimeSpan otStartTime, TimeSpan otEndTime, double totalHours, string? approvalStatus)
        {
            Id = id;
            EmployeeID = employeeID;
            LegalName = legalName;
            Department = department;
            ApprovalStatus = approvalStatus;
            Reason = reason;
            Remarks = remarks;
            AppliedDate = appliedDate;
            OTStartTime = otStartTime;
            OTEndTime = otEndTime;
            TotalHours = totalHours;
        }

        private string? _approvalStatus;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? StatusImage
        {
            get
            {
                return ApprovalStatus switch
                {
                    "Approved" => "approvedleave.png",
                    "Rejected" => "rejectedleave.png",
                    "Pending" => "pendingleave.png",
                    _ => "form.png"
                };
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

