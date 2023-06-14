using Microsoft.AspNetCore.Http;
using Swappa.Infarstructure.Data;

namespace Swappa.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUsers { get; }
        ICategoryRepository Categories { get; }
        IProductsReponsitory Products { get; }
        ISubCategoryRepository SubCategory { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }

        void Save();
        Task<string> UploadedFile(IFormFile ProfilePicture);

    }
}