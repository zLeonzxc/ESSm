namespace ESSmAPI.Models
{
    public record OvertimeRequest
    {
        public int Id { get; init; }
        public string EmployeeID { get; init; }
        public string LegalName { get; init; }
        public string Department { get; init; }
        public string Reason { get; init; }
        public string Remarks { get; init; }
        public DateTime AppliedDate { get; init; }
        public TimeSpan OTStartTime { get; init; }
        public TimeSpan OTEndTime { get; init; }
        public double TotalHours { get; init; }
        public string ApprovalStatus { get; init; }
    }
}
