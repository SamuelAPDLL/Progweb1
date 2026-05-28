namespace Academico.Models;

public class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string SenhaHash { get; set; }
    public int TentativasFalhas { get; set; }

    public DateTime? BloqueadoAte { get; set; }
}