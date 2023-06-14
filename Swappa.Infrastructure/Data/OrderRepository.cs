
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using Swappa.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using Swappa.Infarstructure.Data;
using Swappa.Infrastructure.Interfaces;
using System.Linq.Expressions;
using System.Linq;

#nullable disable

namespace Swappa.Infarstructure.Data
{
    public class OrderRepository : Repository<ApplicationUser>, IOrderRepository
    {

        protected readonly SwappaContext _context;
        protected DbSet<Order> _dbSet;

        public OrderRepository(SwappaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Order>();
        }

        public void Add(Order entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Order FirstOrDefault(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Order GetFirstOrDefault(Expression<Func<Order, bool>> filter, string includeProperties = null, bool tracked = true)
        {

            if (tracked)
            {
                IQueryable<Order> query = _dbSet;
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault()!;
            }
            else
            {
                IQueryable<Order> query = _dbSet.AsNoTracking();
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault()!;
            }
        }

        public void Remove(Order entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public Order SingleOrDefault(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            _dbSet.Update(entity);
        }
       
        Order IOrderRepository.Get(int? id)
        {
            return _dbSet.Find(id)!;

        }
        public Order[] GetArray(int?[] id)
        {
            return _dbSet.Where(o => id.Contains(o.OrderId)).ToArray();
        }
        public void Detach(Order entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }
        IEnumerable<Order> IOrderRepository.GetAll()
        {
            return _dbSet.ToList();

        }

        Task<IEnumerable<Order>> IOrderRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }
        

    }
}