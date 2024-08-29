using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Entity.Security;

[Table("t_auth_refresh_token")]
public class AuthRefreshToken : AuditBase
{
    [Column("token_id", TypeName = "bigint")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TokenId { get; set; }

    [Column("refresh_token", TypeName = "varchar(128)")]
    public string RefreshToken { get; set; } = string.Empty;

    [Column("user_id", TypeName = "varchar(26)")]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public User.User User { get; set; }

    [Column("expiry")]
    public DateTime Expiry{ get; set; }
}