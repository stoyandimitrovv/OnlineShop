using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                var phones = new Category { Name = "Phones", Slug = "phones" };
                var laptops = new Category { Name = "Laptops", Slug = "laptops" };
                var tv = new Category { Name = "Tv", Slug = "tv" };

                context.Products.AddRange(
                        new Product
                        {
                            Name = "IPhone 13",
                            Slug = "iphone13",
                            Description = "IOS 13",
                            Price = 1300M,
                            Category = phones,
                            Image = "iphone13.jpg"
                        },
                        new Product
                        {
                            Name = "IPhone 14",
                            Slug = "iphone14",
                            Description = "IOS 14",
                            Price = 1700M,
                            Category = phones,
                            Image = "iphone14.jpg"
                        },
                        new Product
                        {
                            Name = "IPhone 14 Pro Max",
                            Slug = "iphone14promax",
                            Description = "IOS 14",
                            Price = 2200M,
                            Category = phones,
                            Image = "iphone14promax.jpg"
                        },
                        new Product
                        {
                            Name = "Asus",
                            Slug = "Asus",
                            Description = "18 inch, 16 ram, 512gb SSD",
                            Price = 2000M,
                            Category = laptops,
                            Image = "Asus.jpg"
                        },
                        new Product
                        {
                            Name = "Acer",
                            Slug = "Acer",
                            Description = "15.6 inch, 8 ram, 512gb SSD",
                            Price = 1500M,
                            Category = laptops,
                            Image = "Acer.jpg"
                        },
                        new Product
                        {
                            Name = "Mac",
                            Slug = "Mac",
                            Description = "14 inch, 16 ram, 512gb SSD",
                            Price = 3000M,
                            Category = laptops,
                            Image = "Mac.jpg"
                        },
                        new Product
                        {
                            Name = "Samsung",
                            Slug = "Samsung42",
                            Description = "42 inch",
                            Price = 600M,
                            Category = tv,
                            Image = "Samsung42.jpg"
                        },
                        new Product
                        {
                            Name = "Sony",
                            Slug = "Sony",
                            Description = "50 inch",
                            Price = 750M,
                            Category = tv,
                            Image = "Sony.jpg"
                        },
                        new Product
                        {
                            Name = "Samsung",
                            Slug = "Samsung65",
                            Description = "65 inch",
                            Price = 900M,
                            Category = tv,
                            Image = "Samsung65.jpg"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}