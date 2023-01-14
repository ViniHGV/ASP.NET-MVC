using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ProjetoMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //if(HttpContext.)
            var nome = HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals("user"))?.Value;

            TempData["nomeUsuario"] = nome;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}