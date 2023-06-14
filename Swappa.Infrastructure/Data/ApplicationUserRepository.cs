
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using Swappa.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using Swappa.Infarstructure.Data;
using Swappa.Infrastructure.Interfaces;

#nullable disable

namespace Swappa.Infarstructure.Data
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {

    // private new SwappaContext _context;

        public ApplicationUserRepository(SwappaContext context) : base(context)
        {
            // _context = context;
        }
        public async Task<UserVM> GetUsersByPaging(UserParam request) {

            var users = (from user in _context.ApplicationUsers
                    join UserRole in _context.UserRoles on user.Id equals UserRole.UserId
                    join role in _context.Roles on UserRole.RoleId equals role.Id
                    where (String.IsNullOrEmpty(request.FullName) || user.FullName.Contains(request.FullName))
                        && (String.IsNullOrEmpty(request.Role) || role.Name.Contains(request.Role))
                    select new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Address = user.Address,
                        RoleName = role.Name,
                    });
            var total = await users.CountAsync();
            var data = await users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new UserVM
            {
                DataList = data,
                Total = total
            };
                
        }
    }
}