using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectKonsentrasi.Webserver.Models.Database;
public class ListKlinikTujuan
{
    public ulong ID { get; set; } = 0;
    public string NamaKlinik { get; set; } = default!;
}
