using context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace context.Seeders
{
    public static class CuradorSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            // User and Role Manager
            var userManager = service.GetService<UserManager<User>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole("Curador"));
            await roleManager.CreateAsync(new IdentityRole("Utilizador"));

            var curadorEmail = "curador@email.pt";
            var curadorUser = await userManager.FindByEmailAsync(curadorEmail);

            if (curadorUser == null)
            {
                var newCurador = new User
                {
                    UserName = "Curador",
                    Email = curadorEmail,
                    NomeCompleto = "Curador do Sistema",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newCurador, "Curador123!");
                if (result.Succeeded)
                {
                    Console.WriteLine("Criacao do Curador concluida.");
                    await userManager.AddToRoleAsync(newCurador, "Curador");
                }
            }
        }
    }
}
