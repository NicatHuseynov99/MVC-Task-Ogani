using Microsoft.AspNetCore.Mvc;
using Ogani.Data;
using Ogani.Models;
using Ogani.ViewModels;
using System.Linq;
namespace Ogani.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int take)
        {
            var count = 6;
            count += take;
            List<Category> categories = _context.Categories.ToList();
            List<Product> products = _context.Products.OrderByDescending(m => m.Id).Take(count).ToList();
            ShopVM model = new()
            {
                Categories = categories,
                Products = products,
            };
            return View(model);
        }
    }
}
