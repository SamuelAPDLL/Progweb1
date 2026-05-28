using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Discente()
        {
            return View();
        }

        public IActionResult Docentes()
        {
            return View();
        }
        public IActionResult CriarProfessor()
        {
            return View();
        }
    }
}