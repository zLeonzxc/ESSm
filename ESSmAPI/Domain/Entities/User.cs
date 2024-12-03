using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESSmAPI.Domain.Entities
{
    [Table("users")]
    public record User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; init; }
        
        [Column("username")]
        [MaxLength(50)]
        public string? Username { get; init; }

        [Column("email")]
        [MaxLength(100)]
        public required string Email { get; init; }

        [Column("password")]
        [MaxLength(50)]
        public required string Password { get; init; }
    }
}
