namespace ESSmPrototype.Models
{
    public class LeaveRequest : INotifyPropertyChanged
    {
        public LeaveRequest[]? LeaveRequests { get; set; }
        public string? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeePosition { get; set; }
        public string? LeaveApprovalStatus
        {
            get => _leaveApprovalStatus;
            set
            {
                if (_leaveApprovalStatus != value)
                {
                    _leaveApprovalStatus = value;
                    OnPropertyChanged(nameof(LeaveApprovalStatus));
                    OnPropertyChanged(nameof(StatusImage));
                }
            }
        }
        public string? LeaveReason { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }

        public LeaveRequest(string employeeID, string employeeName, string employeePosition, string leaveApprovalStatus, string leaveReason, DateTime appliedDate, DateTime leaveStartDate, DateTime leaveEndDate)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeePosition = employeePosition;
            LeaveApprovalStatus = leaveApprovalStatus;
            LeaveReason = leaveReason;
            AppliedDate = appliedDate;
            LeaveStartDate = leaveStartDate;
            LeaveEndDate = leaveEndDate;
        }

        private string? _leaveApprovalStatus;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? StatusImage
        {
            get
            {
                return LeaveApprovalStatus switch
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
