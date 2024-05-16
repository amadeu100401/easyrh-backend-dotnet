using EasyRh.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyRh.Infra.DataAccess;

public class EasyRhDbContext : DbContext
{
    //DbContextOptions contém as configurações necessárias para conectar ao banco de dados
    public EasyRhDbContext(DbContextOptions options) : base(options) { }

    //Informando a tabela no banco de dados
    public DbSet<User> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Informando ao Entity que ele precisa usar as configurações dessa classem (no caso o propio assembly)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EasyRhDbContext).Assembly);
    }
}
