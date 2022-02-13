using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ICartService _cartService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, ICategoryService categoryService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var token = await HttpContext.GetTokenAsync("");
        var products = await _productService.FindAllProducts(token);
        return View(products);
    }
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var model = await _productService.FindProductById(id,token);
        return View(model);
    }
    [HttpPost]
    [ActionName("Details")]
    [Authorize]
    public async Task<IActionResult> DetailsPost(ProductViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value,
                
            },
          
        };
        var productDebug = await _productService.FindProductById(model.Id, token);
        CartDetailViewModel cartDetail = new CartDetailViewModel()
        {
            Count = model.Count,
            ProductId = model.Id,
            Product = await _productService.FindProductById(model.Id,token),
            CartHeaderId = 0,
            CartHeader = null,
            Category = await _categoryService.FindCategoryById(productDebug.CategoryId)
            
        };

        List<CartDetailViewModel> cartDetails = new();

        cartDetails.Add(cartDetail);
        cart.CartDetails = cartDetails;
        var response = await _cartService.AddItemToCart(cart, token);
        if (response != null)
        {
            return RedirectToAction(nameof(Index));
        }


        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [Authorize] 
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}

