using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using ProjetoMVC.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjetoMVC.DAL.Repositories;

namespace ProjetoMVC

{
    public class Program
    {
        public const string CookieScheme = "UserScheme";
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UsuarioDBContext>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //// Add services to the container.
            //builder.Services.AddDbContext<UsuarioDBContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

            builder.Services.AddControllersWithViews();

            
            builder.Services.AddAuthentication(CookieScheme)
           .AddCookie(CookieScheme, options =>
           {
                options.AccessDeniedPath = "/Login/AcessoNegado";
                options.LoginPath = "/Login/Index";
                options.LogoutPath = "/Login/logout";
           });

            builder.Services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, CookieConfiguration>();

            var app = builder.Build();
            //app.UseCookiePolicy();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            



            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Login}/{action=Login}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            Migrate(builder.Services);

            app.Run();
        }

        /// <summary>
        /// Executa a migração do DB automaticamente ao rodar a aplicação
        /// </summary>
        /// <param name="services"></param>
        private static void Migrate(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var db = serviceProvider.GetRequiredService<UsuarioDBContext>();
            db.Database.Migrate();

            db.Database.EnsureCreated();
            Console.WriteLine(db.Database.GetAppliedMigrations());
            
        }
    }

    
}