using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers;

public class AlunoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}