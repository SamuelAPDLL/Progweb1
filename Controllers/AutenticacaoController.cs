using Academico.Models;
using Academico.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academico.Controllers;

public class AutenticacaoController : Controller
{
    private readonly UsuarioRepository _usuarioRepository;

    public AutenticacaoController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        Usuario? usuario = _usuarioRepository.BuscarPorEmail(model.Email);

        if (usuario != null && usuario.BloqueadoAte > DateTime.Now)
{
    TempData["Mensagem"] = "Usuário bloqueado. Tente novamente mais tarde.";
    TempData["Tipo"] = "warning";
    return View(model);
}

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.SenhaHash))
        {

            if (usuario != null)
    {
        usuario.TentativasFalhas++;

        if (usuario.TentativasFalhas >= 3)
        {
            usuario.BloqueadoAte = DateTime.Now.AddMinutes(1);
            usuario.TentativasFalhas = 0;
        }

        _usuarioRepository.Atualizar(usuario);
    }

            TempData["Mensagem"] = "Email ou senha inválidos.";
            TempData["Tipo"] = "danger";
            return View(model);
        }

                usuario.TentativasFalhas = 0;
        usuario.BloqueadoAte = null;

        _usuarioRepository.Atualizar(usuario);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        ClaimsIdentity identidade = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        ClaimsPrincipal principal = new ClaimsPrincipal(identidade);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastroUsuarioViewModel model)
    {
        Usuario? usuarioExistente = _usuarioRepository.BuscarPorEmail(model.Email);

        if (usuarioExistente != null)
        {
            ModelState.AddModelError("", "Já existe um usuário com esse email.");
            return View(model);
        }

        Usuario usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha)
        };

        _usuarioRepository.Cadastrar(usuario);

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Index", "Home");
    }
}