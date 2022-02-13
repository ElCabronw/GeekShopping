using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService , ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }


        
        public async Task<IActionResult> ProductIndex()
        {
        
            var products = await _productService.FindAllProducts("");
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            var categoriesList = await _categoryService.FindAllCategories();
            ViewBag.CategoriesList = categoriesList;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            // if (ModelState.IsValid)
            // {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.CreateProduct(model,token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
           // }
            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id,token);
            var categoriesList = await _categoryService.FindAllCategories();
            ViewBag.CategoriesList = categoriesList;

            if(product != null) return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            // if (ModelState.IsValid)
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.UpdateProduct(model,token);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
            
            return View(model);
        }


        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id,token);
            var categoriesList = await _categoryService.FindAllCategories();
            ViewBag.CategoriesList = categoriesList;

            if (product != null) return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            // if (ModelState.IsValid)
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id,token);
            if (response) return RedirectToAction(nameof(ProductIndex));

            return View(model);
        }




    }
}

