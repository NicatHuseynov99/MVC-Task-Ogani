using Microsoft.AspNetCore.Mvc;
using Ogani.Data;
using Ogani.Models;
using Ogani.ViewModels;

namespace Ogani.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Banner> banners = _context.Banners.ToList();
            return View(banners);
        }
    }
}
