using Microsoft.AspNetCore.Mvc;
using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Academico.Controllers;

[Authorize]
public class ProfessorController : Controller
{
    readonly IProfessorRepository _professorRepository;
    public ProfessorController(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }
        public async Task<IActionResult> Index()
    {
        var professor = await _professorRepository.GetAllProfessoresAsync();
        return View(professor);
    }

    public IActionResult CriarProfessor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarProfessorAsync(Professor professor)
    {
        if(await _professorRepository.CriarProfessorAsync(professor))
         {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Professor {professor.Nome} cadastrado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Professor {professor.Nome} não cadastrado cadastrado!";
        }
        return RedirectToAction("CriarProfessor");
    }

    public IActionResult AtualizarProfessor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarProfessorAsync(Professor professor)
    {
        if (await _professorRepository.AtualizarProfessorAsync (professor))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"professor {professor.Nome} atualizado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"professor {professor.Nome} não atualizado!";
        }
        return RedirectToAction("Atualizarprofessor");
    }
      
    public async Task<IActionResult> ExcluirProfessorAsync(int Id)
    {
        if (await _professorRepository.ExcluirProfessorAsync (Id))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno deletado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Aluno falha em deletar!";
        }
        return RedirectToAction("Index");
    }
}