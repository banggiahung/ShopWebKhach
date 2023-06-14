
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
    public class SubCategoryRepository : Repository<ApplicationUser>, ISubCategoryRepository
    {

        protected readonly SwappaContext _context;
        protected DbSet<SubCategory> _dbSet;

        public SubCategoryRepository(SwappaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<SubCategory>();
        }

        public void Add(SubCategory entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<SubCategory> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> Find(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public SubCategory FirstOrDefault(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubCategory> GetAll(Expression<Func<SubCategory, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public SubCategory GetFirstOrDefault(Expression<Func<SubCategory, bool>> filter, string includeProperties = null, bool tracked = true)
        {

            if (tracked)
            {
                IQueryable<SubCategory> query = _dbSet;
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
                IQueryable<SubCategory> query = _dbSet.AsNoTracking();
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

        public void Remove(SubCategory entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<SubCategory> entities)
        {
            throw new NotImplementedException();
        }

        public SubCategory SingleOrDefault(Expression<Func<SubCategory, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(SubCategory entity)
        {
            _dbSet.Update(entity);
        }
       
        SubCategory ISubCategoryRepository.Get(int? id)
        {
            return _dbSet.Find(id)!;

        }
        public void Detach(SubCategory entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }
        IEnumerable<SubCategory> ISubCategoryRepository.GetAll()
        {
            return _dbSet.ToList();

        }

        Task<IEnumerable<SubCategory>> ISubCategoryRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<SubCategory> GetAllWithProduct()
        {
            return _context.SubCategories
                .Include(p => p.Product) // Kết hợp bảng Subcategory
                .Include(p => p.Category) // Kết hợp bảng Subcategory
                .Select(p => new SubCategory
                {
                    SubCategoryId = p.SubCategoryId,
                    SubCategory_Name = p.SubCategory_Name,
                    Category = new Category
                    {
                        Category_Name = p.Category.Category_Name 
                    },
                    Product = p.Product.Select(prod => new Product
                    {
                        Product_Name = prod.Product_Name,
                        Product_ImagePath = prod.Product_ImagePath,
                        Product_Price = prod.Product_Price,
                    }).ToList()
                })
                .ToList();
        }

    }
}