using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AspNetCoreWithIdentity.Models;

namespace AspNetCoreWithIdentity
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";

            if (await roleManager.FindByNameAsync("administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("administrator"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                var createUserResult = await userManager.CreateAsync(admin, password);
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, new [] { "administrator", "user" });
                }
            }
        }
    }
}
