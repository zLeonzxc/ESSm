namespace ESSmAPI.Models
{
    public record Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string LegalName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }



    }
}
