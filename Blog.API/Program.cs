using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tHost = CreateHostBuilder(args).Build();

            try
            {
                var tScope = tHost.Services.CreateScope();
                var tContext = tScope.ServiceProvider.GetRequiredService<RepositoryContext>();
                var tUserManager = tScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var tRoleManager = tScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Database must exist
                tContext.Database.EnsureCreated();

                // Seed "Admin" role and "admin" user
                var tAdminRole = new IdentityRole("Admin");
                if (!tContext.Roles.Any())
                {
                    tRoleManager.CreateAsync(tAdminRole).GetAwaiter().GetResult();
                }

                if (!tContext.Users.Any(u => u.UserName == "admin"))
                {
                    var tAdminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@blog.com",
                    };

                    // Default password: (proper password validation has been disabled under IdentityUser configuration options)
                    tUserManager.CreateAsync(tAdminUser, "secret").GetAwaiter().GetResult();
                    // Add role to user
                    tUserManager.AddToRoleAsync(tAdminUser, tAdminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            tHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
