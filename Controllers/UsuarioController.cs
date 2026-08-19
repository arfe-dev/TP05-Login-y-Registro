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
        return View();
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

    public IActionResult Registro()
    {
        return View();
    }



    [HttpPost] public IActionResult Registro(Usuario usuario)
    {
        //Comprobar la base de datos para ver si el nombre de usuario ya existe. Si existe, retornar la vista Registro con un mensaje de error, sino guardar el usuario en la base de datos y redirigir a la vista Login.
        BD bd = new BD();
        if (bd.ExisteUsuario(usuario.NombreUsuario))
        {
            ViewBag.Error = "El nombre de usuario ya existe.";
            return View(usuario);
        }

        bd.RegistrarUsuario(usuario);
        return RedirectToAction("Login");
    }

    


    //Un metodo  IActionResult Bienvenida(), el cual tenga un IF para HttpContext.Session.GetString("Usuario") == null, y que si lo es, retornar la vista Login, de lo contrario retornar la vista View.)
    public IActionResult Bienvenida()
    {
        if (HttpContext.Session.GetString("Usuario") == null)
        {
            return RedirectToAction("Login");
        }
        else
        {
            return View();
        }
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

}