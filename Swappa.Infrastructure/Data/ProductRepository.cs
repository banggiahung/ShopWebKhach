
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
    public class ProductRepository : Repository<ApplicationUser>, IProductsReponsitory
    {

        protected readonly SwappaContext _context;
        protected DbSet<Product> _dbSet;

        public ProductRepository(SwappaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public void Add(Product entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Product FirstOrDefault(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Product GetFirstOrDefault(Expression<Func<Product, bool>> filter, string includeProperties = null, bool tracked = true)
        {

            if (tracked)
            {
                IQueryable<Product> query = _dbSet;
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
                IQueryable<Product> query = _dbSet.AsNoTracking();
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

        public void Remove(Product entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Product SingleOrDefault(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            _dbSet.Update(entity);
        }
       
        Product IProductsReponsitory.Get(int? id)
        {
            return _dbSet.Find(id)!;

        }
        public void Detach(Product entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        IEnumerable<Product> IProductsReponsitory.GetAll()
        {
            return _dbSet.ToList();

        }

        Task<IEnumerable<Product>> IProductsReponsitory.GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Product> GetAllWithSubcategory()
        {
            return _context.Products
                .Include(p => p.SubCategory) // Kết hợp bảng Subcategory
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    Product_Number = p.Product_Number,
                    Product_Name = p.Product_Name,
                    Product_Description = p.Product_Description,
                    Product_Price = p.Product_Price,
                    Product_Status = p.Product_Status,
                    Product_Quantity = p.Product_Quantity,
                    SubCategoryId = p.SubCategoryId,
                    Product_ImagePath = p.Product_ImagePath,
                    SubCategory = new SubCategory
                    {
                        SubCategory_Name = p.SubCategory.SubCategory_Name,
                       // Lấy ra tên của subcategory
                    }
                })
                .ToList();
        }
       public Product GetWithId(int productId)
        {
            return _context.Products
                .Include(p => p.SubCategory)
                .FirstOrDefault(p => p.ProductId == productId);
        }
    }
}