namespace ESSmPrototype.Models
{
    public class LeaveRequest : INotifyPropertyChanged
    {
        public string? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeDep { get; set; }
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
        public string? LeaveComment { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }

        public LeaveRequest(string employeeID, string employeeName, string employeeDep, string leaveApprovalStatus, string leaveReason, DateTime appliedDate, DateTime leaveStartDate, DateTime leaveEndDate, string leavecomment)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeDep = employeeDep;
            LeaveApprovalStatus = leaveApprovalStatus;
            LeaveReason = leaveReason;
            AppliedDate = appliedDate;
            LeaveStartDate = leaveStartDate;
            LeaveEndDate = leaveEndDate;
            LeaveComment = leavecomment;
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
