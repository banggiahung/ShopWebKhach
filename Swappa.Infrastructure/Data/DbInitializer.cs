#nullable disable

using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Swappa.Infarstructure.Data
{
    public class DbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SwappaContext _context;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SwappaContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public void Initialize()
        {
            // Migrations if they are not applied
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

            // Create roles if they are not created
            if (!_roleManager.RoleExistsAsync(WebsiteRole.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRole.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRole.Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRole.Customer)).GetAwaiter().GetResult();

                // If roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@ltvaz.com",
                    Email = "admin@ltvaz.com",
                    FullName = "Admin",
                    PhoneNumber = "0987654321",
                }, "Admin123@#").GetAwaiter().GetResult(); 
                ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@ltvaz.com");
 
                _userManager.AddToRoleAsync(user, WebsiteRole.Admin).GetAwaiter().GetResult();

            }
            return;
        }
    }
}
