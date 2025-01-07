namespace ESSmPrototype.Models;

    public class OTRequest
    {
        public int Id { get; set; }
        public string? EmployeeID { get; set; }
        public string? LegalName { get; set; }
        public string? Department { get; set; }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime OTStartTime { get; set; }
        public DateTime OTEndTime { get; set; }
        public double TotalHours { get; set; }
        public string? ApprovalStatus { get; set; }

    }
