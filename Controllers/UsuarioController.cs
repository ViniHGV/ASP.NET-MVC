using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.DAL.Repositories;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
   // [Authorize(AuthenticationSchemes = Program.CookieScheme, Roles = )]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [Authorize]
        public IActionResult List()
        {
            List<Usuario> usuarios = new List<Usuario>();
            _usuarioRepository.GetAll().ToList().ForEach(p =>{
                usuarios.Add(EntityToModel(p));
            });

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Insert(ModelToEntity(usuario));

                return RedirectToAction(nameof(List));
            }
            return View(usuario);
        }

        public IActionResult Editar(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            //Retornar um erro na tela, ou retornar para a página de listagem
            if (usuario == null)
                return NotFound();

            return View(EntityToModel(usuario));
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            _usuarioRepository.Update(ModelToEntity(usuario));

            return RedirectToAction(nameof(List));
        }

        public IActionResult Excluir(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
                return RedirectToAction(nameof(List));

            return View(EntityToModel(usuario));

        }

        [HttpPost]
        public IActionResult Excluir(Usuario usuario)
        {
            bool deletou = _usuarioRepository.Delete(usuario.Id);

            return RedirectToAction(nameof(List));
        }

        public IActionResult EsqueceuSenha()
        {
            return View();
        }

        /// <summary>
        /// TODO:Validar e-mail cadastrado, se existir enviar email, senao mostrar erro
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EsqueceuSenha(string email)
        {
            ViewData["statusEmail"] = "true";
            ViewData["mensagem"] = "Email enviado com sucesso";
            return View();
        }

        /*
         * TODO: métodos podem ir para um lugarzim melhor, hm depois voltamos aqui :P
        */
        private ProjetoMVC.DAL.Entities.Usuario ModelToEntity(Usuario usuario)
        {
            ProjetoMVC.DAL.Entities.Usuario usuarioEntity = new DAL.Entities.Usuario
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            return usuarioEntity;
        }
        private Usuario EntityToModel(ProjetoMVC.DAL.Entities.Usuario usuarioEntity)
        {
            Usuario usuario = new Usuario
            {
                Id = usuarioEntity.Id,
                Nome = usuarioEntity.Nome,
                Email = usuarioEntity.Email,
                Senha = usuarioEntity.Senha
            };

            return usuario;
        }
    }
}
