using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using System.Linq.Expressions;

namespace Swappa.Infrastructure.Interfaces
{
    public interface IOrderRepository : IRepository<ApplicationUser>
    {
        Order Get(int? id);
        Order[] GetArray(int?[] id);

        Order GetFirstOrDefault(Expression<Func<Order, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAll(Expression<Func<Order, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<Order>> GetAllAsync();
        IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate);
        Order SingleOrDefault(Expression<Func<Order, bool>> predicate);
        Order FirstOrDefault(Expression<Func<Order, bool>> predicate);
        void Add(Order entity);
        void AddRange(IEnumerable<Order> entities);
        void Update(Order entity);
        void Remove(Order entity);
        void RemoveRange(IEnumerable<Order> entities);
        void Detach(Order entity);

    }
}