
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Swappa.ApplicationCore.Models;
using Swappa.Infrastructure.Interfaces;

namespace Swappa.Infarstructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SwappaContext _context;
        private readonly IWebHostEnvironment _iHostingEnvironment;

        public UnitOfWork(SwappaContext context, IWebHostEnvironment iHostingEnvironment)
        {
            _context = context;
            ApplicationUsers = new ApplicationUserRepository(_context);
            Categories = new CategoryRepository(_context);
            Products = new ProductRepository(_context);
            SubCategory = new SubCategoryRepository(_context);
            Order = new OrderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            _iHostingEnvironment = iHostingEnvironment;
        }


        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IProductsReponsitory Products { get; private set; }
        public ISubCategoryRepository SubCategory { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        


        /// <summary>
        /// Here is where we commit a transaction.
        /// </summary>
        /// <returns></returns>

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<string> UploadedFile(IFormFile ProfilePicture)
        {
            string ProfilePictureFileName = null;

            if (ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot/upload");

                if (ProfilePicture.FileName == null)
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + "blank-person.png";
                else
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, ProfilePictureFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePicture.CopyTo(fileStream);
                }
            }
            return ProfilePictureFileName;
        }

       
    }
}
