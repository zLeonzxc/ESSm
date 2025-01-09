namespace ESSmAPI.Models
{
    public record Company
    {
        public int Id { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }

    }
}
