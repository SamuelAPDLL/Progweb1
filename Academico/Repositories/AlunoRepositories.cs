using Academico.Models;

namespace Academico.Repositories;

public class AlunoRepository : IAlunoRepository
{
    readonly AcademicoContext _context;

    public AlunoRepository(AcademicoContext context)
    {
        _context = context;
    }
    public async Task CriarAlunoAsync(Aluno aluno)
    {
        await _context.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }
    
}

public interface IAlunoRepository
{
    Task CriarAlunoAsync(Aluno aluno);
}