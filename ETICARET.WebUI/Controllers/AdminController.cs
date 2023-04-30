using ETICARET.Business.Abstract;
using ETICARET.Entities;
using ETICARET.WebUI.Identity;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("admin/products")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ProductList()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
            
        }

        public IActionResult EditProduct(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetProductDetails(id);

            if(entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Images = entity.Images,
                SelectedCategory = entity.ProductCategories.Select(i => i.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model,List<IFormFile> files, int[] categoryIds)
        {
            var entity = _productService.GetById(model.Id);

            if(entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;

            if(files != null)
            {
                foreach (var file in files)
                {
                    Image image = new Image();
                    image.ImageUrl = file.FileName;
                    entity.Images.Add(image);
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _productService.Update(entity,categoryIds);
                return RedirectToAction("ProductList");

            }

            return View(entity);

        }
        public IActionResult DeleteProduct(int productId)
        {
            var product = _productService.GetById(productId);
            _productService.Delete(product);

            return RedirectToAction("ProductList");
        }
    }
}
