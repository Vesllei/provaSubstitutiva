
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


public class Context : DbContext
{

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<IMC> IMCs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=weslley.db");

    }
}
