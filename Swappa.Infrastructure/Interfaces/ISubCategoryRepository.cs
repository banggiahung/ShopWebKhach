using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using System.Linq.Expressions;

namespace Swappa.Infrastructure.Interfaces
{
    public interface ISubCategoryRepository : IRepository<ApplicationUser>
    {
        SubCategory Get(int? id);

        SubCategory GetFirstOrDefault(Expression<Func<SubCategory, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<SubCategory> GetAll();

        IEnumerable<SubCategory> GetAll(Expression<Func<SubCategory, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<SubCategory>> GetAllAsync();
        IEnumerable<SubCategory> Find(Expression<Func<SubCategory, bool>> predicate);
        SubCategory SingleOrDefault(Expression<Func<SubCategory, bool>> predicate);
        SubCategory FirstOrDefault(Expression<Func<SubCategory, bool>> predicate);
        void Add(SubCategory entity);
        void AddRange(IEnumerable<SubCategory> entities);
        void Update(SubCategory entity);
        void Remove(SubCategory entity);
        void RemoveRange(IEnumerable<SubCategory> entities);
        void Detach(SubCategory entity);
        IEnumerable<SubCategory> GetAllWithProduct();

    }
}