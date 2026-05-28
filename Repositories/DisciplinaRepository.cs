using System.Xml.Linq;
using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    readonly AcademicoContext _context;

    public DisciplinaRepository(AcademicoContext context)
    {
        _context = context;
    }
    public async Task<bool> CriarDisciplinaAsync(Disciplina disciplina,  int professorId)
    {
        disciplina.Professor = await _context.Professor.FirstOrDefaultAsync(x => x.Id == professorId);
        await _context.AddAsync(disciplina);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Disciplina>>GetAllDisciplinaAsync()
    {
        return await _context.Disciplina.Include(d => d.Professor).ToListAsync();
    }

public async Task<bool> AtualizarDisciplinaAsync(Disciplina disciplina)
    {
        var disciplinaBanco = await _context.Disciplina.FirstOrDefaultAsync(x => x.Id == disciplina.Id);
        disciplinaBanco!.Nome = disciplina.Nome;
        disciplinaBanco.CargaHoraria = disciplina.CargaHoraria;
        disciplinaBanco.Professor = disciplina.Professor;
        disciplinaBanco.Alunos = disciplina.Alunos;
        disciplinaBanco.Periodo = disciplina.Periodo;
        _context.Disciplina.Update(disciplinaBanco);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirDisciplinaAsync(int id)
    {
        var linhasAfetadas = await _context.Disciplina
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return linhasAfetadas > 0;
    }
}

public interface IDisciplinaRepository
{
    Task<bool> CriarDisciplinaAsync(Disciplina disciplina, int ProfessorId);
    Task<List<Disciplina>>GetAllDisciplinaAsync();
    Task<bool>AtualizarDisciplinaAsync(Disciplina disciplina);
    Task<bool>ExcluirDisciplinaAsync(int id);
}