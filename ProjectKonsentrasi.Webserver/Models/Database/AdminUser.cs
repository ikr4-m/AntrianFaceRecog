using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectKonsentrasi.Webserver.Models.Database;
public class AdminUser
{
    public ulong ID { get; set; } = 0;
    public string Nama { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public DateTime CreatedAt { get; set; } = default!;
}
