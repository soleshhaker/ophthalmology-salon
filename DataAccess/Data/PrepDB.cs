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
                var admin = await userManager.FindByNameAsync(adminUserName);
                if (admin == null)
                {
                    admin = new ApplicationUser() { UserName = adminUserName, Email = "admin@admin.com", Role = SD.Role_Admin };
                    await userManager.CreateAsync(admin, "Admin123!");
                }

                var isInAdminRole = await userManager.IsInRoleAsync(admin, SD.Role_Admin);
                if (!isInAdminRole)
                {
                    await userManager.AddToRoleAsync(admin, SD.Role_Admin);
                }

                const string doctorUserName = "Doctor";
                var doctor = await userManager.FindByNameAsync(doctorUserName);
                if (doctor == null)
                {
                    doctor = new ApplicationUser() { UserName = doctorUserName, Email = "doctor@doctor.com", Role = SD.Role_Doctor };
                    await userManager.CreateAsync(doctor, "Doc123!");
                }

                var isInDoctorRole = await userManager.IsInRoleAsync(doctor, SD.Role_Doctor);
                if (!isInDoctorRole)
                {
                    await userManager.AddToRoleAsync(doctor, SD.Role_Doctor);
                }

                const string customerUserName = "Customer";
                var customer = await userManager.FindByNameAsync(customerUserName);
                if (customer == null)
                {
                    customer = new ApplicationUser() { UserName = customerUserName, Email = "customer@customer.com", Role = SD.Role_Customer };
                    await userManager.CreateAsync(customer, "Customer123!");
                }

                var isInCustomerRole = await userManager.IsInRoleAsync(customer, SD.Role_Customer);
                if (!isInCustomerRole)
                {
                    await userManager.AddToRoleAsync(customer, SD.Role_Customer);
                }
            }
            
        }
    }
}
