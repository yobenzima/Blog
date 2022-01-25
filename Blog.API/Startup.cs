using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Contracts;
using Blog.Entities;
using Blog.Repository;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Blog")));

            _ = services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    // These options are on by default - disabled for simplicity when seeding IdentityUser
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RepositoryContext>();

            // Inject Repository Wrapper
            _ = services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            // Inject AutoMapper
            _ = services.AddAutoMapper(typeof(Startup));
            
            _ = services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        _ = builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
