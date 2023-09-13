using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider);
            }
        }

        private static async void SeedData(IServiceProvider serviceProvider)
        {
            using (var dbContext = serviceProvider.GetRequiredService<ApplicationDBContext>())
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roleExist = roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult();
                if (!roleExist)
                {
                    // Create the Admin Role
                    await roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                }

                const string adminUserName = "Admin";
                var user = await userManager.FindByNameAsync(adminUserName);
                if (user == null)
                {
                    user = new ApplicationUser() { UserName = adminUserName, Email = "admin@admin.com" };
                    await userManager.CreateAsync(user, "Admin123!");
                }

                var isInAdminRole = await userManager.IsInRoleAsync(user, SD.Role_Admin);
                if (!isInAdminRole)
                {
                    await userManager.AddToRoleAsync(user, SD.Role_Admin);
                }
            }
            
        }
    }
}
