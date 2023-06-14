using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swappa.ApplicationCore.Utilities;
using Swappa.Infrastructure.Interfaces;
using System.Data;

namespace Swappa.Web.Controllers
{
  

    // This controller contains the uow class containing our SwappaContext. 
    public abstract class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }
    }
}
