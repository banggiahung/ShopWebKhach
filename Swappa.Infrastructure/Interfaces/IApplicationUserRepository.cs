using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;

namespace Swappa.Infrastructure.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
         Task<UserVM> GetUsersByPaging(UserParam request);
    }
}