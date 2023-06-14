using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using System.Linq.Expressions;

namespace Swappa.Infrastructure.Interfaces
{
    public interface IOrderDetailRepository : IRepository<ApplicationUser>
    {
        OrderDetail Get(int? id);

        OrderDetail GetFirstOrDefault(Expression<Func<OrderDetail, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<OrderDetail> GetAll();

        IEnumerable<OrderDetail> GetAll(Expression<Func<OrderDetail, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<OrderDetail>> GetAllAsync();
        IEnumerable<OrderDetail> Find(Expression<Func<OrderDetail, bool>> predicate);
        OrderDetail SingleOrDefault(Expression<Func<OrderDetail, bool>> predicate);
        OrderDetail FirstOrDefault(Expression<Func<OrderDetail, bool>> predicate);
        void Add(OrderDetail entity);
        void AddRange(IEnumerable<OrderDetail> entities);
        void Update(OrderDetail entity);
        void Remove(OrderDetail entity);
        void RemoveRange(IEnumerable<OrderDetail> entities);
        void Detach(OrderDetail entity);
        IEnumerable<OrderDetail> GetAllWithUserId(string userId);


    }
}