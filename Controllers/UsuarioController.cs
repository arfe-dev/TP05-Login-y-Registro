using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP05.Models;

namespace TP05.Controllers;

public class UsuarioController : Controller
{
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost] public IActionResult Login(Usuario usuario)
    {
        // Después vamos a consultar la Base de Datos
        // para comprobar NombreUsuario y Contraseña.

        // EJEMPLO TEMPORAL:
        if (usuario.NombreUsuario == "admin" && usuario.Contraseña == "1234")
        {
            HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);

            return RedirectToAction("Bienvenida");
        }
        else{
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(usuario);
        }
       
        
    }

    public IActionResult Registro()
    {
        return View();
    }



    [HttpPost] public IActionResult Registro(Usuario usuario)
    {
        // Acá después vamos a comprobar en la BD
        // si el NombreUsuario ya existe.

        // Y después guardar el usuario en la BD.

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