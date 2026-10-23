using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using MojaApp.API.Data;

using MojaApp.API.Dtos;

using MojaApp.API.Models;



namespace MojaApp.API.Controllers;



[ApiController]

[Route("[controller]")]

public class StudentiController(AppDbContext db) : ControllerBase

{

    [HttpGet]

    public async Task<List<StudentResponse>> GetAll(CancellationToken ct)

    {

        List<Student> studenti = await db.Studenti.ToListAsync(ct); // SELECT 

        return studenti.Select(Mapiraj).ToList();

    }



    [HttpGet("{id:int}")]

    public async Task<ActionResult<StudentResponse>> GetById(int id, CancellationToken ct)

    {

        Student? student = await db.Studenti.FirstOrDefaultAsync(x => x.Id == id, ct); // SELECT ... WHERE Id = @id 



        if (student == null)

            return NotFound();



        return Mapiraj(student);

    }



    [HttpPost]

    public async Task<ActionResult<StudentResponse>> Dodaj(StudentDodajRequest request, CancellationToken ct)

    {

        Student student = new()

        {

            Ime = request.Ime,

            Prezime = request.Prezime,

            BrojIndeksa = request.BrojIndeksa,

            DatumRodjenja = request.DatumRodjenja,

            DatumKreiranja = DateTime.UtcNow,

        };



        db.Studenti.Add(student);

        await db.SaveChangesAsync(ct); // INSERT; baza dodjeljuje Id 



        return CreatedAtAction(nameof(GetById), new { id = student.Id }, Mapiraj(student));

    }



    private static StudentResponse Mapiraj(Student s) => new()

    {

        Id = s.Id,

        ImePrezime = $"{s.Ime} {s.Prezime}",

        BrojIndeksa = s.BrojIndeksa,

        DatumRodjenja = s.DatumRodjenja,

    };

}