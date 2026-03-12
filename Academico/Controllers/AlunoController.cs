using Microsoft.AspNetCore.Mvc;

namespace Academico.Models;

public class AlunoController : Controller
{
    public IActionResult Index()
    {
        List<Aluno> aluno1 = new List<Aluno>()
        {
        new Aluno()
        {
        Nome = "Samuel Alexandre Pudell",
        Cpf = "89067823401",
        Curso = "Tecnologia em Análise e Desenvolvimento de Sistemas",
        Matricula = "2025122024330013",
        DataNascimento = new DateOnly( 2003, 01, 16)
    },
    new Aluno()
    {
    Nome = "Cleifer Silva Moreira",
        Cpf = "16485231695",
        Curso = "Tecnologia em Análise e Desenvolvimento de Sistemas",
        Matricula = "2025122024330025",
        DataNascimento = new DateOnly( 2002, 04, 24)        
    }
        };      
        return View(aluno1);
    }

}