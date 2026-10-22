using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Data;
using MojaApp.API.Dtos;
using MojaApp.API.Models;

namespace MojaApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentiController(StudentStorage storage) : ControllerBase
{
    [HttpGet]
    public List<StudentResponse> GetAll()
    {
        return storage.GetAll().Select(Mapiraj).ToList();
    }

    [HttpGet("{id:int}")]
    public ActionResult<StudentResponse> GetById(int id)
    {
        Student? student = storage.GetById(id);

        if (student == null)
            return NotFound(); // 404, a ne izuzetak (500)

        return Mapiraj(student);
    }

    [HttpPost]
    public ActionResult<StudentResponse> Dodaj(StudentDodajRequest request)
    {
        // [ApiController] sam vraća 400 ako request ne prođe validaciju iz StudentDodajRequest

        // Format je ispravan, ali se sukobljava s postojećim podatkom -> 409, ne 400
        if (storage.PostojiBrojIndeksa(request.BrojIndeksa))
            return Conflict($"Student s brojem indeksa {request.BrojIndeksa} već postoji.");
        Student student = storage.Dodaj(new Student
        {
            Ime = request.Ime,
            Prezime = request.Prezime,
            BrojIndeksa = request.BrojIndeksa,
            DatumRodjenja = request.DatumRodjenja,
        });

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, Mapiraj(student)); // 201 + Location
    }

    [HttpDelete("{id:int}")]
    public IActionResult Obrisi(int id)
    {
        if (!storage.Obrisi(id))
            return NotFound();

        return NoContent(); // 204: uspjelo, nema tijela odgovora
    }

    [HttpPut("{id:int}")]
    public IActionResult Uredi(int id, StudentUrediRequest request)
    {
        Student? student = storage.GetById(id);

        if (student == null)
            return NotFound();

        // storage vraća isti objekat koji je u listi, pa je izmjena odmah sačuvana
        student.Ime = request.Ime;
        student.Prezime = request.Prezime;
        student.DatumRodjenja = request.DatumRodjenja;

        return NoContent(); // 204
    }

    private static StudentResponse Mapiraj(Student s) => new()
    {
        Id = s.Id,
        ImePrezime = $"{s.Ime} {s.Prezime}",
        BrojIndeksa = s.BrojIndeksa,
        DatumRodjenja = s.DatumRodjenja,
    };
}