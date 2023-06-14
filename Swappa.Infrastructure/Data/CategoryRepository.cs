
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.ParamModels;
using Swappa.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using Swappa.Infarstructure.Data;
using Swappa.Infrastructure.Interfaces;
using System.Linq.Expressions;

#nullable disable

namespace Swappa.Infarstructure.Data
{
    public class CategoryRepository : Repository<ApplicationUser>, ICategoryRepository
    {

    // private new SwappaContext _context;

      
        protected readonly SwappaContext _context;
        protected DbSet<Category> _dbSet;

        public CategoryRepository(SwappaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Category>();
        }

        public void Add(Category entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Category FirstOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll(Expression<Func<Category, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Category GetFirstOrDefault(Expression<Func<Category, bool>> filter, string includeProperties = null, bool tracked = true)
        {

            if (tracked)
            {
                IQueryable<Category> query = _dbSet;
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
                IQueryable<Category> query = _dbSet.AsNoTracking();
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

        public void Remove(Category entity)
        {
            _dbSet.Remove(entity);

        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            _dbSet.Update(entity);
        }

        Category ICategoryRepository.Get(int? id)
        {
            return _dbSet.Find(id)!;

        }
        public void Detach(Category entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }
        IEnumerable<Category> ICategoryRepository.GetAll()
        {
            return _dbSet.ToList();

        }

        Task<IEnumerable<Category>> ICategoryRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }


    }
}