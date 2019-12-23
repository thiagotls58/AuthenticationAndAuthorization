using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationAndAuthorization.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            //adicionando o serviço de cookies na aplicação
            //se o usuário não estiver logado, ele é redirecionado para a página de login
            services
                .AddAuthorization(options =>
                {
                    // https://www.red-gate.com/simple-talk/dotnet/c-programming/policy-based-authorization-in-asp-net-core-a-deep-dive/
                    options.AddPolicy("MasterUser", policy =>
                          policy.RequireClaim("Role", new string[] { "Master" }));
                    options.AddPolicy("Manager", policy =>
                          policy.RequireClaim("Role", new string[] { "Diretor", "Gerente" }));
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.Cookie.Name = "ThiagoAdim";
                    options.LoginPath = "/Authentication/Autenticar";
                });

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/error-handling?view=aspnetcore-3.1
            //Tratamento de errors 404...
            app.UseStatusCodePagesWithRedirects("/Error/StatusCode?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Autenticar}/{id?}");
            });
        }
    }
}
