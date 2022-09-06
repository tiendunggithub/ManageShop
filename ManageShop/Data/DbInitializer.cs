using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageShop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
        //context.Database.EnsureCreated();
        // Look for any Products.
            if (context.Products.Any())
            {
                return;
            }
            var categories = new Category[] {
                new Category { Name = "Áo khoác" },
                new Category { Name = "Quần nam" },
                new Category { Name = "Phụ kiện" },
                new Category { Name = "Áo thun" },
                new Category { Name = "Chân váy" }

            };
            foreach (Category i in categories)
            {
                context.Categories.Add(i);
            }
            context.SaveChanges();
            var products = new Product[] {
                new Product { NameProduct = "Áo Bomber", Price = 200000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Áo khoác").CategoryID },
                new Product { NameProduct = "Quần jean ống suông", Price = 350000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Quần nam").CategoryID },
                new Product { NameProduct = "Áo khoác gió chống nước", Price = 160000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Áo khoác").CategoryID },
                new Product { NameProduct = "Áo khoác jean thời trang", Price = 500000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Áo khoác").CategoryID },
                new Product { NameProduct = "Nhẫn titan", Price = 50000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Phụ kiện").CategoryID },
                new Product { NameProduct = "Áo thun vũ trụ - unisex", Price = 50000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Áo thun").CategoryID },
                new Product { NameProduct = "Váy có giây rút", Price = 80000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Chân váy").CategoryID },
                new Product { NameProduct = "Áo Khoác Dù BOMBER THÊU RỒNG Unisex", Price = 180000,
                Description = "", CategoryID = categories.Single( i => i.Name == "Áo khoác").CategoryID }
            };
            foreach (Product s in products)
            {
                context.Products.Add(s);
            }
            context.SaveChanges();


            
            var users = new User[] {
                new User { username = "admin", password = "123",
                fullname = "admin" },
                new User { username = "tiendung", password = "000",
                fullname = "tiendung"  }
                
            };
            foreach (User d in users)
            {
                context.Users.Add(d);
            }
            context.SaveChanges();
        }
    }
}
