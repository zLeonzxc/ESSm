using System.ComponentModel.DataAnnotations;

namespace ESSmAPI.Models
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string CompanyCode { get; set; }

    }
}
