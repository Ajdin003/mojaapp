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
        Student student = storage.Dodaj(new Student
        {
            Ime = request.Ime,
            Prezime = request.Prezime,
            BrojIndeksa = request.BrojIndeksa,
            DatumRodjenja = request.DatumRodjenja,
        });

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, Mapiraj(student)); // 201 + Location
    }

    private static StudentResponse Mapiraj(Student s) => new()
    {
        Id = s.Id,
        ImePrezime = $"{s.Ime} {s.Prezime}",
        BrojIndeksa = s.BrojIndeksa,
        DatumRodjenja = s.DatumRodjenja,
    };
}
