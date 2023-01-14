using Microsoft.EntityFrameworkCore;
using ProjetoMVC.DAL.Contexts;
using ProjetoMVC.DAL.Entities;

namespace ProjetoMVC.DAL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {
        private readonly UsuarioDBContext _context;

        public UsuarioRepository(UsuarioDBContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<UsuarioDBContext>();
            this._context = context; // new UsuarioDBContext(optionsBuilder.Options);
        }
        public bool Delete(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null) return false;

            _context.Remove(usuario);
            _context.SaveChanges();

            return true;
        }
        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuario.ToList();
        }
        public Usuario GetById(int id)
        {
            return _context.Usuario.Find(id);
        }
        public void Insert(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }
        public void Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public List<Usuario> GetByName(string name)
        {
            return _context.Usuario.Where(p=>p.Nome.Contains(name)).ToList();
        }
        public Usuario GetByEmail(string email)
        {
            return _context.Usuario.Where(p => p.Email.Equals(email)).FirstOrDefault();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Este bloco é muito importante, pois garante que todos os recursos utilizados serão 
        /// liberados após a execução.
        /// Isso  é feito pelo GC(garbage colector) automaticamente, mas no caso de banco de dados
        /// o quanto antes os recursos são liberados, menos chances de erros relacionados a banco 
        /// ocorrerão
        /// </summary>
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
