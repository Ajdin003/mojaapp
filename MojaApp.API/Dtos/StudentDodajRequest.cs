using System.ComponentModel.DataAnnotations;

namespace MojaApp.API.Dtos;

// Ono što klijent šalje. Id i DatumKreiranja postavlja server, pa ih ovdje nema.
public class StudentDodajRequest
{
    [Required, StringLength(50)]
    public string Ime { get; set; } = "";

    [Required, StringLength(50)]
    public string Prezime { get; set; } = "";

    [Required, RegularExpression(@"^IB\d{6}$", ErrorMessage = "Broj indeksa mora biti u formatu IB123456.")]
    public string BrojIndeksa { get; set; } = "";

    public DateOnly? DatumRodjenja { get; set; }
}
