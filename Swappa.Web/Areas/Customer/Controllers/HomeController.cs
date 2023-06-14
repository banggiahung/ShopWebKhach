using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Swappa.ApplicationCore.Models;
using Swappa.ApplicationCore.Utilities;
using Swappa.Infarstructure.Data;
using Swappa.Infrastructure.Interfaces;
using Swappa.Web.Models;

namespace Swappa.Web.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
    private readonly SwappaContext _context;
    private readonly IUnitOfWork _uow;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(SwappaContext context, IUnitOfWork uow, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _uow = uow;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        if (User.IsInRole("Customer"))
        {
            return View();
        }
        else if (User.IsInRole("Admin"))
        {
            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml");
        }
        else
        {
            return View();
        }
    }
    [Authorize(Roles = WebsiteRole.Customer)]

    public IActionResult Enter_Email()
    {
        return View();
    }
    [Authorize(Roles = WebsiteRole.Customer)]

    [HttpPost]
    public IActionResult CheckEmail(string email)
    {
        bool dataExists = _context.ApplicationUsers.Any(u => u.Email == email);
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1) // Set cookie expiration time
        };
        Response.Cookies.Append("UserEmail", email, cookieOptions);

        if (dataExists)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        else
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult CartDetails()
    {
        return View();
    }
    [HttpGet]
    public IActionResult DetailsProduct(int id)
    {

        if (id > 0)
        {
            try
            {
                var vm = _uow.Products.GetWithId(id);
                if (vm == null)
                {
                    return BadRequest("Không tìm thấy đối tượng với ID tương ứng");
                }
                return View(vm);

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
    }
    [HttpPost]
    public IActionResult UpdateCartQuantity()
    {
        // Logic để tăng số lượng trong giỏ hàng
        
        int currentQuantity = HttpContext.Session.GetInt32("CartQuantity") ?? 0;
        int newQuantity = currentQuantity + 1;

        // Lưu số lượng mới vào session
        HttpContext.Session.SetInt32("CartQuantity", newQuantity);

        return Json(newQuantity); // Trả về số lượng mới dưới dạng JSON
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromForm] Order model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);
                if (applicationUser != null)
                {
                    model.Customer_Id = userId;
                    model.Customer_Name = applicationUser.FullName;
                    model.Phone_Number = applicationUser.PhoneNumber;
                    model.Address = applicationUser.Address;
                    model.Order_Status = false;
                }

                _uow.Order.Add(model);
                _uow.Save();
                int orderId = model.OrderId;

                List<OrderDetail> orderDetails = new List<OrderDetail>();

                var product = _uow.Products.GetFirstOrDefault(p => p.ProductId == model.ProductIds);
                OrderDetail orderDetail = new OrderDetail
                {
                    Order_Id = orderId,
                    OrderId = orderId,
                    ProductId = product.ProductId,
                    Product_Name = product.Product_Name,
                    Product_Price = product.Product_Price,
                    Product_Quantity = model.QuantityClient,
                    Product_Image = product.Product_ImagePath
                    // Các trường khác của OrderDetail
                };

                orderDetails.Add(orderDetail);
                _uow.OrderDetail.AddRange(orderDetails);
                _uow.Save();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                // Chuyển đối tượng model thành JSON
                var json = System.Text.Json.JsonSerializer.Serialize(model, options);

                // Trả về kết quả dưới dạng JsonResult
                return new JsonResult(json);
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
    public IActionResult DeleteOrdersAndOrderDetails([FromBody] DeleteRequest request)
    {
        try
        {
            var orderDetailsToDelete = _context.OrderDetails.Where(od => request.OrderDetailIds.Contains(od.OrderDetailId)).ToList();
            _context.OrderDetails.RemoveRange(orderDetailsToDelete);

            var ordersToDelete = _context.Orders.Where(o => request.OrderIds.Contains(o.OrderId)).ToList();
            _context.Orders.RemoveRange(ordersToDelete);

            _context.SaveChanges();

            return Ok("xóa thành cônng.");
        }
        catch (Exception ex)
        {
            return BadRequest("Lỗi: " + ex.Message);
        }
    }
    
    public class DeleteRequest
    {
        public List<int> OrderIds { get; set; }
        public List<int> OrderDetailIds { get; set; }
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        try
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var vm = _uow.OrderDetail.GetAllWithUserId(userId);
                return Ok(vm);
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
    public IActionResult CheckOut()
    {
        return  View();
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult ComfirmOrder(int?[] id)
    {
        try
        {
            if (id != null)
            {
                var find = _uow.Order.GetArray(id);
                if (find != null)
                {
                    foreach(var item in find)
                    {
                        item.Order_Status = true;
                        _uow.Order.Update(item);
                        _context.SaveChanges();
                    }
                   
                    return Ok(find);
                }
                else
                {
                    return NotFound();

                }

            }
            else
            {
                return NotFound();

            }
        }
        catch (Exception ex)
        {
            return BadRequest("Lỗi: " + ex.Message);
        }
    }

}
