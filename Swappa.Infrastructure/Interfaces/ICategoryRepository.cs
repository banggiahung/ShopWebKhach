using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using System.Linq.Expressions;

namespace Swappa.Infrastructure.Interfaces
{
    public interface ICategoryRepository : IRepository<ApplicationUser>
    {
        Category Get(int? id);

        Category GetFirstOrDefault(Expression<Func<Category, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<Category> GetAll();

        IEnumerable<Category> GetAll(Expression<Func<Category, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<Category>> GetAllAsync();
        IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate);
        Category SingleOrDefault(Expression<Func<Category, bool>> predicate);
        Category FirstOrDefault(Expression<Func<Category, bool>> predicate);
        void Add(Category entity);
        void AddRange(IEnumerable<Category> entities);
        void Update(Category entity);
        void Remove(Category entity);
        void RemoveRange(IEnumerable<Category> entities);
        void Detach(Category entity);
    }
}