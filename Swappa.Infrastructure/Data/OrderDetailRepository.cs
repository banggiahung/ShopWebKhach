
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using Swappa.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using Swappa.Infarstructure.Data;
using Swappa.Infrastructure.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Swappa.Infarstructure.Data
{
    public class OrderDetailRepository : Repository<ApplicationUser>, IOrderDetailRepository
    {

        protected readonly SwappaContext _context;
        protected DbSet<OrderDetail> _dbSet;

        public OrderDetailRepository(SwappaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<OrderDetail>();
        }

        public void Add(OrderDetail entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<OrderDetail> entities)
        {
            _dbSet.AddRange(entities);

        }

        public IEnumerable<OrderDetail> Find(Expression<Func<OrderDetail, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public OrderDetail FirstOrDefault(Expression<Func<OrderDetail, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetAll(Expression<Func<OrderDetail, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetFirstOrDefault(Expression<Func<OrderDetail, bool>> filter, string includeProperties = null, bool tracked = true)
        {

            if (tracked)
            {
                IQueryable<OrderDetail> query = _dbSet;
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
                IQueryable<OrderDetail> query = _dbSet.AsNoTracking();
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

        public void Remove(OrderDetail entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<OrderDetail> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public OrderDetail SingleOrDefault(Expression<Func<OrderDetail, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetail entity)
        {
            _dbSet.Update(entity);
        }

        OrderDetail IOrderDetailRepository.Get(int? id)
        {
            return _dbSet.Find(id)!;

        }
        public void Detach(OrderDetail entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }
        IEnumerable<OrderDetail> IOrderDetailRepository.GetAll()
        {
            return _dbSet.ToList();

        }

        Task<IEnumerable<OrderDetail>> IOrderDetailRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetAllWithUserId(string userId)
        {
            // Lấy tất cả OrderDetails có Order_Status là false và thuộc về userId
            List<OrderDetail> orderDetails = _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.Order_Status == false && od.Order.Customer_Id == userId)
                .ToList();

            // Gom nhóm các OrderDetails theo ProductId và cộng tổng Quantity
            List<OrderDetail> groupedOrderDetails = orderDetails
       .GroupBy(od => od.ProductId)
       .Select(g => new OrderDetail
       {
           ProductId = g.Key,
           Product_Name = g.First().Product_Name,
           Product_Price = g.First().Product_Price,
           Product_Quantity = g.Sum(od => od.Product_Quantity),
           Product_Image = g.First().Product_Image,
           OrderDetailIds = g.Select(od => od.OrderDetailId).ToList(),
           OrderIds = g.Select(od => od.OrderId).ToList()
       })
       .ToList();



            return groupedOrderDetails;
        }
    }
}