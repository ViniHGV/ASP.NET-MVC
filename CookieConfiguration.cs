using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace ProjetoMVC
{
    public class CookieConfiguration : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        public void Configure(string name, CookieAuthenticationOptions options)
        {
           
        }

        public void Configure(CookieAuthenticationOptions options)=> Configure(Options.DefaultName, options);
    }
}
