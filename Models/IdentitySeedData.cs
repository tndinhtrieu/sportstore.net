using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            string adminUser = "Admin";
            string adminPassword = "123456";
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                UserManager<ApplicationUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                ApplicationUser user = await userManager.FindByIdAsync(adminUser);
                if (user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = adminUser;
                    user.Email = "tndinhtrieu@gmail.com";
                    user.EmailConfirmed = true;
                    await userManager.CreateAsync(user, adminPassword);
                }
            }
        }
    }
}
