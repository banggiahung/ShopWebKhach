using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using System.Linq.Expressions;

namespace Swappa.Infrastructure.Interfaces
{
    public interface IProductsReponsitory : IRepository<ApplicationUser>
    {
        Product Get(int? id);

        Product GetFirstOrDefault(Expression<Func<Product, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(Expression<Func<Product, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<Product>> GetAllAsync();
        IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate);
        Product SingleOrDefault(Expression<Func<Product, bool>> predicate);
        Product FirstOrDefault(Expression<Func<Product, bool>> predicate);
        void Add(Product entity);
        void AddRange(IEnumerable<Product> entities);
        void Update(Product entity);
        void Remove(Product entity);
        void RemoveRange(IEnumerable<Product> entities);
        void Detach(Product entity);
        IEnumerable<Product> GetAllWithSubcategory();
        Product GetWithId(int productId);
    }
}