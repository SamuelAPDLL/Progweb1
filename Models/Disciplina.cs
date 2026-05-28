using Academico.Models;
using Microsoft.AspNetCore.SignalR;

namespace Academico.Models;

public class Disciplina
{
    public int Id {get; set;}
    public string Nome {get; set; } = "";
    public int CargaHoraria {get; set; }
    public Professor? Professor {get; set; }
    public List<Aluno>? Alunos {get; set; }
    public string Periodo {get; set;} = "";
    public string Codigo { get; set; } ="";
}