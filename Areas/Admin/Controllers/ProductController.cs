using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ogani.Data;
using Ogani.Models;
using Ogani.Utilities.File;
using Ogani.Utilities.Helpers;
using Ogani.Utilities.Paginate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ogani.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, int take = 5)
        {
            int number = 1 + (page - 1) * take;
            List<Product> products = _context.Products
                .Include(m => m.Category)
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            int count = (int)Math.Ceiling((decimal)_context.Products.Count() / take);
            Pagination<Product> result = new Pagination<Product>(products, page, count,number);
            return View(result);
        }
        public IActionResult Read(int id)
        {
            Product? product = _context.Products.Include(m => m.Category).FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                NotFound();
            }
            return View(product);
        }
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is wrong");
                return View(model);
            }
            if (!model.Photo.CheckFileSize(3500))
            {
                ModelState.AddModelError("Photo", "File size is wrong");
                return View(model);
            }
            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "img/product", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
            }
            model.Image = fileName;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            Product? product = _context.Products.Find(id);
            if (product == null)
            {
                NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product model)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            Product? product = await _context.Products.FindAsync(id);
            if (model.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo is required");
                return View(product);
            }
            else
            {
                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type is wrong");
                    return View(model);
                }
                if (!model.Photo.CheckFileSize(3500))
                {
                    ModelState.AddModelError("Photo", "File size is wrong");
                    return View(model);
                }
            }
            if (!ModelState.IsValid)
            {
                model.Image = product.Image;
                return View(model);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "img/product", product.Image);
            Helper.DeleteFile(path);
            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string newPath = Helper.GetFilePath(_env.WebRootPath, "img/product", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
            }
            product.Image = fileName;
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Weight = model.Weight;
            product.Category = model.Category;
            product.InStock = model.InStock;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Disable(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.InStock = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Active(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            Category? category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            if (category.IsActive == true)
            {
                product.InStock = true;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "img/categories", product.Image);
            Helper.DeleteFile(path);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
