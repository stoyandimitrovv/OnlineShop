using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Services.Contracts;

namespace ShoppingCart.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<Product>> GetProductsForAdmin(int p, int pageSize)
        {
            return await _context.Products.OrderByDescending(p => p.Id)
                .Include(p => p.Category)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Create(Product product)
        {

            if (product.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                string filePath = Path.Combine(uploadsDir, imageName);

                FileStream fs = new FileStream(filePath, FileMode.Create);
                await product.ImageUpload.CopyToAsync(fs);
                fs.Close();

                product.Image = imageName;
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            if (product.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                string filePath = Path.Combine(uploadsDir, imageName);

                FileStream fs = new FileStream(filePath, FileMode.Create);
                await product.ImageUpload.CopyToAsync(fs);
                fs.Close();

                product.Image = imageName;
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            Product product = await _context.Products.FindAsync(id);

            if (!string.Equals(product.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string oldImagePath = Path.Combine(uploadsDir, product.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task RateProduct(long id, long rating, string userId)
        {
            var newRating = new Rating
            {
                UserId = userId,
                ProductId = id,
                RatingValue = rating
            };

            _context.Ratings.Add(newRating);
            await _context.SaveChangesAsync();
        }
    }
}
