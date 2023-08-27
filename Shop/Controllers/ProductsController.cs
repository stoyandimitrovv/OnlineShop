using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using Microsoft.AspNet.Identity;
using ShoppingCart.Services.Contracts;

namespace ShoppingCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IProductService _productService;

        public ProductsController(DataContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;

            var ratings = await _context.Ratings.ToListAsync();
            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);
                var products = await _context.Products.ToListAsync();
                products.ForEach(p =>
                {
                    p.Rating = ratings?.LastOrDefault(r=>r.UserId == User.Identity.GetUserId() && r.ProductId==p.Id)?.RatingValue;
                });
                return View(products.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize));
            }

            Category category = await _context.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");

            var productsByCategory = await _context.Products.Where(p => p.CategoryId == category.Id).ToListAsync();
            productsByCategory.ForEach(p =>
            {
                p.Rating = ratings?.LastOrDefault(r => r.UserId == User.Identity.GetUserId() && r.ProductId == p.Id)?.RatingValue;
            });

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);

            return View(productsByCategory.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> RateProduct(long id, long rating)
        {
            await _productService.RateProduct(id, rating, User.Identity.GetUserId());

            return RedirectToAction("Index");
        }
    }
}
