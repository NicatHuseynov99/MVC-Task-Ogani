using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.Data;
using Ogani.Models;
namespace Ogani.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public HeroViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = await _context.Categories.Where(m => m.IsActive == true).ToListAsync();
            return await Task.FromResult(View(categories));
        }
    }
}
