using System.ComponentModel.DataAnnotations.Schema;

namespace ESSmAPI.Models
{
    //[Table("users")]
    public record User
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column("id)")]
        //public int Id { get; init; }

        //[Column("user_id")]
        //public required string UserId { get; init; }

        //[Column("username")]
        //[MaxLength(50)]
        //public string? Username { get; init; }

        //[Column("email")]
        //[MaxLength(100)]
        //public required string Email { get; init; }

        //[Column("password")]
        //[MaxLength(50)]
        //public required string Password { get; init; }

        //[Column("created_at")]
        //public DateTime CreatedAt { get; init; }

        //[Column("updated_at")]
        //public DateTime UpdatedAt { get; init; }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public bool IsLoggedIn { get; set; }
        public string CompanyCode { get; set; }
    }
}
