using System.Reflection.Metadata.Ecma335;
using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Academico.Controllers;

[Authorize]
public class AlunoController : Controller
{
    readonly IAlunoRepository _alunoRepository;
    public AlunoController(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }
        public async Task<IActionResult> Index()
    {
       var aluno = await _alunoRepository.GetAllAlunosAsync();
        return View(aluno);
    }

    public IActionResult CriarAluno()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarAlunoAsync(Aluno aluno)
    {
        if (await _alunoRepository.CriarAlunoAsync(aluno))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} cadastrado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} não cadastrado cadastrado!";
        }
        return RedirectToAction("CriarAluno");
    }

    public IActionResult AtualizarAluno()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarAlunoAsync(Aluno aluno)
    {
        if (await _alunoRepository.AtualizarAlunoAsync (aluno))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} atualizado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} não atualizado!";
        }
        return RedirectToAction("AtualizarAluno");
    }
      
    public async Task<IActionResult> ExcluirAlunoAsync(int Id)
    {
        if (await _alunoRepository.ExcluirAlunoAsync (Id))
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