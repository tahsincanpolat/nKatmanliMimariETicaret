using ETICARET.Business.Abstract;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETICARET.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService) // Injection
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(new ProductListModel() { Products = _productService.GetAll() });
        }


    }
}