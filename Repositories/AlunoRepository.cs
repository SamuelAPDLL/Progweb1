using Academico.Models;
using System.Security.Cryptography.X509Certificates;

using Microsoft.EntityFrameworkCore;
namespace Academico.Repositories;

public class AlunoRepository : IAlunoRepository
{
    readonly AcademicoContext _context;

    public AlunoRepository(AcademicoContext context)
    {
        _context = context;
    }
    public async Task<bool> CriarAlunoAsync(Aluno aluno)
    {
        aluno.Matricula = $"{DateTime.Now.Year}{_context.Aluno.CountAsync().Result}{new Random().Next(0, 99)}";
        await _context.AddAsync(aluno);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Aluno>>GetAllAlunosAsync()
    {
        return await _context.Aluno.ToListAsync();
    }

public async Task<bool> AtualizarAlunoAsync(Aluno aluno)
    {
        var alunoBanco = await _context.Aluno.FirstOrDefaultAsync(x => x.Id == aluno.Id);
        alunoBanco!.Nome = aluno.Nome;
        alunoBanco.Cpf = aluno.Cpf;
        alunoBanco.Curso = aluno.Curso;
        alunoBanco.DataNascimento = aluno.DataNascimento;
        _context.Aluno.Update(alunoBanco);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExcluirAlunoAsync(int id)
    {
        var linhasAfetadas = await _context.Aluno
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return linhasAfetadas > 0;
    }
}

public interface IAlunoRepository
{
    Task<bool> CriarAlunoAsync(Aluno aluno);
    Task<List<Aluno>>GetAllAlunosAsync();
    Task<bool>AtualizarAlunoAsync(Aluno aluno);
    Task<bool>ExcluirAlunoAsync(int id);
}