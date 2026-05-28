using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repository;

public class UsuarioRepository
{
    private readonly AcademicoContext _context;

    public UsuarioRepository(AcademicoContext context)
    {
        _context = context;
    }

    public void Cadastrar(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        _context.SaveChanges();
    }

    public void Atualizar(Usuario usuario)
{
    _context.Usuario.Update(usuario);
    _context.SaveChanges();
}

    public Usuario? BuscarPorEmail(string email)
    {
        return _context.Usuario
            .FirstOrDefault(u => u.Email == email);
    }
}