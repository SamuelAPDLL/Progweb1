using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace Academico.models;

public class Aluno
{
    public string nome {get; set;}
    public string matricula {get; set;}
    public string cpf {get; set;}
    public DateOnly dataNascimento {get; set;}
}