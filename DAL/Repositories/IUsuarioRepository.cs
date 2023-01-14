using ProjetoMVC.DAL.Entities;

namespace ProjetoMVC.DAL.Repositories
{
    public interface IUsuarioRepository : IDisposable
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        List<Usuario> GetByName(string name);
        public Usuario GetByEmail(string email);
        void Insert(Usuario usuario);
        void Update(Usuario usuario);
        bool Delete(int id);
        
    }
}
