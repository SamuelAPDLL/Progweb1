using Microsoft.EntityFrameworkCore;

namespace Academico.Models;
public class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions options)
    : base(options)
    {
    }
    public DbSet<Professor> Professor { get; set; }
    public DbSet<Aluno> Aluno { get; set; }
}
