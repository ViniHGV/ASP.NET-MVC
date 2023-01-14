using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.DAL.Repositories;
using ProjetoMVC.Models;
using System.Security.Claims;

namespace ProjetoMVC.Controllers
{
    public class LoginController : Controller { 

        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (ValidarLogin(loginModel.Email, loginModel.Senha, out Usuario user))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("email", user.Email),
                            new Claim("user", user.Nome),
                            new Claim("role", "user")
                        };

                        HttpContext.SignInAsync(new ClaimsPrincipal(
                            new ClaimsIdentity(claims, "Cookies", "name", "user"))
                            );

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["MensagemErro"] = "Usuário ou senha inválido(s), tente novamente";
                    }
                }
                
                return View("Login");
            }
            catch (Exception)
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao efetivar o login, contate o administrador para maiores detalhes";
                
                return View("Login");
            }
        }
        public IActionResult AcessoNegado(string returnUrl = null)
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private bool ValidarLogin(string email, string senha, out Usuario usuario)
        {
            usuario = EntityToModel(_usuarioRepository.GetByEmail(email));
            return usuario != null;
            
        }

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
