using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESSmAPI.Models
{
    [Table("employees")]
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
