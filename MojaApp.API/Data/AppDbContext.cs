using Microsoft.EntityFrameworkCore;
using MojaApp.API.Models;

namespace MojaApp.API.Data;

// Most između C# koda i baze: svaki DbSet je jedna tabela.
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Student> Studenti => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Početni podaci: migracija ih upisuje u tabelu.
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Ime = "Ana", Prezime = "Anić", BrojIndeksa = "IB260001", DatumRodjenja = new DateOnly(2006, 3, 14), DatumKreiranja = new DateTime(2026, 10, 1) },
            new Student { Id = 2, Ime = "Edin", Prezime = "Edić", BrojIndeksa = "IB260002", DatumKreiranja = new DateTime(2026, 10, 1) });
    }
}