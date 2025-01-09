namespace ESSmAPI.Models
{
    public record User
    {

        public int Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? Username { get; set; }
        public byte[]? Pw { get; set; }
        public string? CompanyCode { get; set; }
        public bool IsLoggedIn { get; set; }

    }
}
