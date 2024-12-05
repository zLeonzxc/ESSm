using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESSmAPI.Models
{
    [Table("employees")]
    public record Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; init; }

        [Column("employee_id")]
        public required string EmployeeId { get; init; }

        [Column("employee_name")]
        [MaxLength(50)]
        public required string Name { get; init; }

        [Column("employee_email")]
        [MaxLength(100)]
        public required string Email { get; init; }

        [Column("employee_department")]
        [MaxLength(20)]
        public string? Department { get; init; }

        [Column("created_at")]
        public DateTime CreatedAt { get; init; }

        [Column("employee_status")]
        public bool IsActiveEmployee { get; init; }

        [Column("user_id")]
        public int UserId { get; init; }

        [ForeignKey("UserId")]
        public User? User { get; init; }


    }
}
