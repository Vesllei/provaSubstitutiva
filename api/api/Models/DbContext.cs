
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=nicolas_nicolas.db");
    }
}