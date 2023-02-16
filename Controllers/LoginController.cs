using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;
using System;

namespace ProjetoMVC.Controllers
{
    public class LoginController : Controller
    {
        //private readonly AgendaContext _context;

        [HttpPost]
        public IActionResult Logar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (loginModel.Email == "Vns@" && loginModel.Senha == "123" )
                    {
                        return RedirectToAction("Index", "Usuario");
                    }
                    TempData["MensagemErro"] = $"Usuario ou senha errado tente novamente!";
                }
                return View("Index");
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro:";
                return RedirectToAction("Editar");
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
