using System.Reflection.Metadata.Ecma335;
using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Academico.Controllers;

[Authorize]
public class DisciplinaController : Controller
{
    readonly IDisciplinaRepository _disciplinaRepository;
    readonly IProfessorRepository _professorRepository;
    public DisciplinaController(IDisciplinaRepository disciplinaRepository, IProfessorRepository professorRepository)
    {
        _disciplinaRepository = disciplinaRepository;
        _professorRepository = professorRepository;
    }
        public async Task<IActionResult> Index()
    {
       var disciplina = await _disciplinaRepository.GetAllDisciplinaAsync();
        return View(disciplina);
    }

    public async Task<IActionResult> CriarDisciplinaAsync()
    {
        ViewBag.Professor = new SelectList(
            await _professorRepository.GetAllProfessoresAsync(),
            "Id",
            "Nome"
        );

        return View();
    }

    [HttpPost]
public async Task<IActionResult> CriarDisciplinaAsync(DisciplinaViewModel disciplinaViewModel)
{
    Disciplina disciplina = new()
    {
        Nome = disciplinaViewModel.Nome,
        CargaHoraria = disciplinaViewModel.CargaHoraria,
        Periodo = disciplinaViewModel.Periodo,
    };

    await _disciplinaRepository.CriarDisciplinaAsync(
        disciplina,
        disciplinaViewModel.ProfessorId
    );

    disciplina.Codigo = $"DISC-{disciplina.Id}";

    await _disciplinaRepository.AtualizarDisciplinaAsync(disciplina);

    return RedirectToAction("CriarDisciplina");
}

    [HttpPost]
    public async Task<IActionResult> AtualizarDisciplinaAsync(Disciplina disciplina)
    {
        if (await _disciplinaRepository.AtualizarDisciplinaAsync (disciplina))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Disciplina {disciplina.Nome} atualizado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Disciplina {disciplina.Nome} não atualizado!";
        }
        return RedirectToAction("AtualizarDisciplina");
    }
      
    public async Task<IActionResult> ExcluirDisciplinaAsync(int Id)
    {
        if (await _disciplinaRepository.ExcluirDisciplinaAsync (Id))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Disciplina deletado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Disciplina falha em deletar!";
        }
        return RedirectToAction("Index");
    }
}