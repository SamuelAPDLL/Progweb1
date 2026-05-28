using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    readonly AcademicoContext _context;

    public ProfessorRepository(AcademicoContext context)
    {
        _context = context;
    }
    public async Task<bool> CriarProfessorAsync(Professor professor)
    {
        await _context.AddAsync(professor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Professor>> GetAllProfessoresAsync()
    {
        return await _context.Professor.ToListAsync();
    }

public async Task<bool> AtualizarProfessorAsync(Professor professor)
    {
        var professorBanco = await _context.Professor.FirstOrDefaultAsync(x => x.Id == professor.Id);
        professorBanco!.Nome = professor.Nome;
        professorBanco.Cpf = professor.Cpf;
        professorBanco.Siape = professor.Siape;
        professorBanco.DataNascimento = professor.DataNascimento;
        professorBanco.Area = professor.Area;
        _context.Professor.Update(professorBanco);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirProfessorAsync(int id)
    {
        var linhasAfetadas = await _context.Professor
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return linhasAfetadas > 0;
    }
}

public interface IProfessorRepository
{
    Task<bool> CriarProfessorAsync(Professor Professor);
    Task<List<Professor>>GetAllProfessoresAsync();
    Task<bool>AtualizarProfessorAsync(Professor professor);
    Task<bool>ExcluirProfessorAsync(int id);
}