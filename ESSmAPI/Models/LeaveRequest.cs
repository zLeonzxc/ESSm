namespace ESSmAPI.Models
{
    public record LeaveRequest
    {
        public int Id { get; set; }
        public string? EmployeeID { get; set; }
        public string? LegalName { get; set; }
        public string? Department { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
    }
}
