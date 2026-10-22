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

    public List<Student> GetAll(string? pretraga = null)
    {
        if (string.IsNullOrWhiteSpace(pretraga))
            return _studenti;

        string trazi = pretraga.Trim();

        return _studenti
            .Where(x => x.Ime.Contains(trazi, StringComparison.OrdinalIgnoreCase)
                     || x.Prezime.Contains(trazi, StringComparison.OrdinalIgnoreCase)
                     || x.BrojIndeksa.Contains(trazi, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Student? GetById(int id) => _studenti.FirstOrDefault(x => x.Id == id);

    public Student Dodaj(Student student)
    {
        student.Id = _sljedeciId++;
        student.DatumKreiranja = DateTime.UtcNow;
        _studenti.Add(student);
        return student;
    }

    public bool Obrisi(int id)
    {
        Student? student = GetById(id);

        if (student == null)
            return false;

        _studenti.Remove(student);
        return true;
    }
}
