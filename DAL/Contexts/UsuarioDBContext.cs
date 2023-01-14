using Microsoft.EntityFrameworkCore;
using ProjetoMVC.DAL.Entities;

namespace ProjetoMVC.DAL.Contexts
{
    public class UsuarioDBContext : DbContext
    {
        public UsuarioDBContext(DbContextOptions<UsuarioDBContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddJsonFile("appsettings.Development.json", optional:true, reloadOnChange:true)
                //.AddUserSecrets()
                .Build();

            var connectionString = configuration.GetConnectionString("ConexaoPadrao");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
