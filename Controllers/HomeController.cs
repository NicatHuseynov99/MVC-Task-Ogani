using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.Data;
using Ogani.Models;
using Ogani.ViewModels;

namespace Ogani.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Where(m=>m.IsActive==true).ToList();
            List<Product> products = _context.Products.Include(m=>m.Category).ToList();
            List<Banner> bannners = _context.Banners.ToList();
            HomeVM model = new()
            {
                Categories = categories,
                Products = products,
                Banners= bannners
            };

            return View(model);
        }
    }
}
