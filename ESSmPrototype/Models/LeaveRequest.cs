namespace ESSmPrototype.Models
{
    public class LeaveRequest : INotifyPropertyChanged
    {
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
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }

        public LeaveRequest(string employeeID, string employeeName, string employeeDep, string leaveApprovalStatus, string leaveReason, DateTime appliedDate, DateTime leaveStartDate, DateTime leaveEndDate, string leavecomment)
        {
            EmployeeID = employeeID;
            LegalName = employeeName;
            Department = employeeDep;
            ApprovalStatus = leaveApprovalStatus;
            Reason = leaveReason;
            AppliedDate = appliedDate;
            LeaveStartDate = leaveStartDate;
            LeaveEndDate = leaveEndDate;
            Remarks = leavecomment;
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
