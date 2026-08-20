using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP05.Models;

namespace TP05.Controllers;

public class UsuarioController : Controller
{

    public IActionResult Index(){
        return View();
    } 

    public IActionResult Login(){
        return View("~/Views/Home/Login.cshtml");
    }

    public IActionResult Registro()
    {
        return View("~/Views/Home/Registro.cshtml");
    }


    [HttpPost] public IActionResult Login(Usuario usuario)
    {
        if (usuario == null)
        {
            ViewBag.Error = "Debe completar los datos del login.";
            return View();
        }

        BD bd = new BD();
        Usuario usuarioBD = bd.ObtenerUsuario(usuario.NombreUsuario);

        if (usuarioBD != null && usuarioBD.Contraseña == usuario.Contraseña)
        {
            HttpContext.Session.SetString("Usuario", usuarioBD.NombreUsuario);
            return RedirectToAction("Bienvenida");
        }

        ViewBag.Error = "Usuario o contraseña incorrectos";
        return View(usuario);
    }

    [HttpPost] public IActionResult Registro(Usuario usuario)
    {
        BD bd = new BD();
        if (bd.ExisteUsuario(usuario.NombreUsuario))
        {
            ViewBag.Error = "El nombre de usuario ya existe.";
            return View(usuario);
        }

        bd.RegistrarUsuario(usuario);
        return RedirectToAction("Login");
    }

    public IActionResult Bienvenida()
    {
        if (HttpContext.Session.GetString("Usuario") == null)
        {
            return RedirectToAction("Login");
        }

        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

