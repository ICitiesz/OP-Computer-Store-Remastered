using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Entity.User;

[Table("t_user")]
public class User
{
    [Column("id", TypeName = "bigint")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("user_id", TypeName = "varchar(36)")]
    public string UserId { get; set; } = string.Empty;

    [Column("username", TypeName = "varchar(16)")]
    public string Username { get; set; } = string.Empty;

    [Column("email", TypeName = "varchar(255)")]
    public string Email { get; set; } = string.Empty;

    [Column("hashed_pass", TypeName = "varchar(86)")]
    public string HashedPass { get; set; } = string.Empty;

    [Column("contact_number", TypeName = "varchar(12)")]
    public string? ContactNumber { get; set; } = string.Empty;

    [Column("gender", TypeName = "varchar(6)")]
    public string? Gender { get; set; } = string.Empty;

    [Column("profile_image", TypeName = "varchar(255)")]
    public string? ProfileImage { get; set; } = string.Empty;

    [Column("role_id", TypeName = "bigint")]
    public long? RoleId { get; set; }
}