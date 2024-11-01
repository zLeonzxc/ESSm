namespace ESSmPrototype.Models
{
    class LeaveRequest 
    {
        private LeaveRequest[]? leaveRequests;
        private string? employeeID;
        private string? employeeName;
        private string? employeePosition;
        private string? leaveApprovalStatus;
        private string? leaveReason;

        public string? EmployeeID
        {
            get => employeeID;
            set
            {
                if (employeeID != value)
                {
                    employeeID = value;
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
                }
            }
        }

        public LeaveRequest(string employeeID, string employeeName, string employeePosition, string leaveApprovalStatus, string leaveReason)
        {
            {
                EmployeeID = employeeID;
                EmployeeName = employeeName;
                EmployeePosition = employeePosition;
                LeaveApprovalStatus = leaveApprovalStatus;
                LeaveReason = leaveReason;
            };
        }
    }
}
