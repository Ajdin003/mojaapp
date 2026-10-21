using MojaApp.API.Models;

namespace MojaApp.API.Data;

// Privremeno skladište u memoriji: podaci nestaju kad se aplikacija ugasi.
// Registrovano kao singleton u Program.cs. Na narednim vježbama ga zamjenjuje baza (EF Core).
public class StudentStorage
{
    private readonly List<Student> _studenti = [];
    private int _sljedeciId = 1;

    public StudentStorage()
    {
        Dodaj(new Student { Ime = "Ana", Prezime = "Anić", BrojIndeksa = "IB260001", DatumRodjenja = new DateOnly(2006, 3, 14) });
        Dodaj(new Student { Ime = "Edin", Prezime = "Edić", BrojIndeksa = "IB260002" });
    }

    public List<Student> GetAll() => _studenti;

    public Student? GetById(int id) => _studenti.FirstOrDefault(x => x.Id == id);

    public Student Dodaj(Student student)
    {
        student.Id = _sljedeciId++;
        student.DatumKreiranja = DateTime.UtcNow;
        _studenti.Add(student);
        return student;
    }
}
