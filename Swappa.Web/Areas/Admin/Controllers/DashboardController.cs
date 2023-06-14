using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swappa.ApplicationCore.Utilities;
using Swappa.ApplicationCore.Models;
using Swappa.Infrastructure.Interfaces;
using Swappa.ApplicationCore.ParamModels;
using Swappa.ApplicationCore.ViewModels;
using Swappa.ApplicationCore.DtoModels;
using SendGrid.Helpers.Mail;
using Microsoft.CodeAnalysis.Differencing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Swappa.Web.Controllers;
[Area("Admin")]
[Authorize(Roles = WebsiteRole.Admin)]
public class DashboardController : BaseController
{
    #region Constructors
    public DashboardController(IUnitOfWork uow) : base(uow)
    {
    }
    #endregion
    // GET: Admin/Movies
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ManageUsers(int id = 1)
    {
        UserParam userParam = new UserParam
        {
            PageIndex = id,
            PageSize = 10
        };
        Task<UserVM> users = _uow.ApplicationUsers.GetUsersByPaging(userParam);
        return View(users.Result);
    }
    public IActionResult ManageCategories()
    {
        var categories = _uow.Categories.GetAll();
        return View(categories);
    }
    public IActionResult CreateUser()
    {
        return View();
    }
    public IActionResult EditProducts()
    {
        return View();
    }
  
  
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllProduct()
    {
        var getList = _uow.Products.GetAllWithSubcategory();
        return Ok(getList);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllProduct2()
    {
        var getList = _uow.Products.GetAllWithSubcategory()
                        .GroupBy(p => p.SubCategoryId); // GroupBy theo SubCategoryId

        return Ok(getList);
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllCate()
    {
        var getList = _uow.Categories.GetAll();
        return Ok(getList);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllSubProduct()
    {
        var getList = _uow.SubCategory.GetAllWithProduct();
        return Ok(getList);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllSubCateProduct()
    {
        var getList = _uow.SubCategory.GetAll();
        return Ok(getList);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getAllSubProductId1()
    {
        var getList = _uow.SubCategory.GetAll().Where(s => s.SubCategoryId == 2); 
        return Ok(getList);
    }
    //Thêm sản phẩm
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product model)
    {
        try
        {
            if (ModelState.IsValid)
            {

                if (model.PrPath != null)
                {
                    var PrPath = await _uow.UploadedFile(model.PrPath);
                    model.Product_ImagePath = "/upload/" + PrPath;
                }
                _uow.Products.Add(model);
                _uow.Save();
                return Ok(model);

            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> UpdateProduct([FromForm] Product model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _uow.Products.Get(model.ProductId);
                if (existingProduct == null)
                {
                    return NotFound("Không tìm thấy");
                }

                if (model.PrPath != null)
                {
                    var PrPath = await _uow.UploadedFile(model.PrPath);
                    model.Product_ImagePath = "/upload/" + PrPath;
                }
                else
                {
                    model.Product_ImagePath = existingProduct.Product_ImagePath;
                }
                _uow.Products.Detach(existingProduct);
                _uow.Products.Update(model);
                _uow.Save();
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getIdProducts(int id)
    {
        try
        {
            Product vm = new Product();
            if (id > 0)
            {
                try
                {
                    vm = _uow.Products.GetFirstOrDefault(p => p.ProductId == id);

                    if (vm == null)
                    {
                        return BadRequest("Không tìm thấy đối tượng với ID tương ứng");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
            {
                return NotFound();
            }


            return Ok(vm);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [AllowAnonymous]

    [HttpPost]
    public async Task<IActionResult> deleteProducts([FromForm] Product model)
    {
        try
        {
            var existingProduct = _uow.Products.Get(model.ProductId);

            if (existingProduct == null)
            {
                return NotFound();

            }
            _uow.Products.Remove(existingProduct);
            _uow.Save();
            
            return Ok(model);


        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    //Thêm sản phẩm
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddCate(Category model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (model.CatePath != null)
                {
                    var CatePath = await _uow.UploadedFile(model.CatePath);
                    model.Category_Image = "/upload/" + CatePath;
                }
                _uow.Categories.Add(model);
                _uow.Save();
                return Ok(model);

            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> UpdateCate([FromForm] Category model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _uow.Categories.Get(model.CategoryId);
                if (existingProduct == null)
                {
                    return NotFound("Không tìm thấy");
                }

                if (model.CatePath != null)
                {
                    var CatePath = await _uow.UploadedFile(model.CatePath);
                    model.Category_Image = "/upload/" + CatePath;
                }
                else
                {
                    model.Category_Image = existingProduct.Category_Image;
                }

                _uow.Categories.Detach(existingProduct);
                _uow.Categories.Update(model);
                _uow.Save();
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getIdCate(int id)
    {
        try
        {
            Category vm = new Category();
            if (id > 0)
            {
                try
                {
                    vm = _uow.Categories.GetFirstOrDefault(p => p.CategoryId == id);

                    if (vm == null)
                    {
                        return BadRequest("Không tìm thấy đối tượng với ID tương ứng");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
            {
                return NotFound();
            }


            return Ok(vm);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [AllowAnonymous]

    [HttpPost]
    public async Task<IActionResult> deleteCate([FromForm] Category model)
    {
        try
        {
            var existingProduct = _uow.Categories.Get(model.CategoryId);

            if (existingProduct == null)
            {
                return NotFound();

            }
            _uow.Categories.Remove(existingProduct);
            _uow.Save();

            return Ok(model);


        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    //Thêm sản phẩm
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddCateSub(SubCategory model)
    {
        try
        {
            if (ModelState.IsValid)
            {                   
              
                _uow.SubCategory.Add(model);
                _uow.Save();
                return Ok(model);

            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> UpdateSubCate([FromForm] SubCategory model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _uow.SubCategory.Get(model.SubCategoryId);
                if (existingProduct == null)
                {
                    return NotFound("Không tìm thấy");
                }

                _uow.SubCategory.Detach(existingProduct);
                _uow.SubCategory.Update(model);
                _uow.Save();
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> getIdCateSub(int id)
    {
        try
        {
            SubCategory vm = new SubCategory();
            if (id > 0)
            {
                try
                {
                    vm = _uow.SubCategory.GetFirstOrDefault(p => p.SubCategoryId == id);

                    if (vm == null)
                    {
                        return BadRequest("Không tìm thấy đối tượng với ID tương ứng");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
            {
                return NotFound();
            }


            return Ok(vm);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [AllowAnonymous]

    [HttpPost]
    public async Task<IActionResult> deleteCateSub([FromForm] SubCategory model)
    {
        try
        {
            var existingProduct = _uow.SubCategory.Get(model.SubCategoryId);

            if (existingProduct == null)
            {
                return NotFound();

            }
            _uow.SubCategory.Remove(existingProduct);
            _uow.Save();

            return Ok(model);


        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
