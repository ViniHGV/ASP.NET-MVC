using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AgendaContext _context;

       


        public UsuarioController(AgendaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var usuarios = _context.Usuario.ToList();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Add(usuarios);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }
        public IActionResult Editar(int id)
        {
            var usuarios = _context.Usuario.Find(id);

            if (usuarios == null)
                return NotFound();

            return View(usuarios);
        }

        [HttpPost]
        public IActionResult Editar(Usuarios usuarios)
        {
            var usuarioBanco = _context.Usuario.Find(usuarios.id);

            usuarioBanco.Nome = usuarios.Nome;
            usuarioBanco.Email = usuarios.Email;
            usuarioBanco.Senha = usuarios.Senha;

            _context.Usuario.Update(usuarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            var usuarios = _context.Usuario.Find(id);

            if (usuarios == null)
                return RedirectToAction(nameof(Index));

            return View(usuarios);
        }

        [HttpPost]
        public IActionResult Excluir(Usuarios usuarios)
        {
            var usuarioBanco = _context.Usuario.Find(usuarios.id);

            _context.Usuario.Remove(usuarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login"); 
        }

    }
}
