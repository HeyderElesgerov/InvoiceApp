using InvoiceApp.Infrastructure.Data.Context;
using InvoiceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                UserManager<AppUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                RoleManager<IdentityRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var newUser = new AppUser()
                {
                    Email = "admin@invoice.app",
                    UserName = "admin",
                    Surname = "admin"
                };
                var result = await _userManager.CreateAsync(newUser, "Admin123.");

                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(UserRole.Admin);

                    if(roleExists)
                        await _userManager.AddToRoleAsync(newUser, UserRole.Admin);
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                        await _userManager.AddToRoleAsync(newUser, UserRole.Admin);
                    }
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
