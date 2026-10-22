using System.ComponentModel.DataAnnotations;

namespace MojaApp.API.Dtos;

// Ono što klijent šalje pri izmjeni. Broja indeksa nema: on se ne mijenja nakon upisa.
public class StudentUrediRequest
{
    [Required, StringLength(50)]
    public string Ime { get; set; } = "";

    [Required, StringLength(50)]
    public string Prezime { get; set; } = "";

    public DateOnly? DatumRodjenja { get; set; }
}
