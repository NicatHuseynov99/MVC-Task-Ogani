using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ogani.Data;
using Ogani.Models;
using Ogani.ViewModels;

namespace Ogani.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<BasketDetailVM> products = new List<BasketDetailVM>();
            if (Request.Cookies["basket"] == null)
            {
                return View(products);
            }
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            foreach (var item in basket)
            {
                Product product = _context.Products.FirstOrDefault(m => m.Id == item.Id);
                BasketDetailVM basketProduct = new BasketDetailVM()
                {
                    Id= product.Id,
                    Name=product.Name,
                    Price=product.Price,
                    Image=product.Image,
                    Count=item.Count,
                };
                products.Add(basketProduct);
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            await UpdateBasket(id);


            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult RemoveBasket(int id)
        {
            List<BasketVM> basket = GetBasket();
            if (basket.Count > 0)
            {
                basket.Remove(basket.FirstOrDefault(m => m.Id == id));
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }
            if (basket.Count == 0)
            {
                Response.Cookies.Delete("basket");
            }
            return RedirectToAction("Index");
        }


        private Task UpdateBasket(int id)
        {
            List<BasketVM> basket = GetBasket();
            BasketVM exitsProduct = basket.FirstOrDefault(m => m.Id == id);
            if (exitsProduct == null)
            {
                basket.Add(new BasketVM { Id = id, Count = 1 });
            }
            else
            {
                exitsProduct.Count++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return Task.CompletedTask;
        }
        private List<BasketVM> GetBasket()
        {
            if (Request.Cookies.ContainsKey("basket"))
            {
                return JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            return new List<BasketVM>();
        }
    }
}
