using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace Academico.Controller

public class AlunoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}