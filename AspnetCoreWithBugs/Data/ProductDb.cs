using AspnetCoreWithBugs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreWithBugs.Data
{
    public static class ProductDb
    {
        public static async Task<List<Product>> GetAllClothing(ProductContext context)       
        {
            List<Product> products =
                await context.Product
                      .OrderBy(p => p.ProductId)
                      .ToListAsync();

            return products;
        }

        public static async Task<Product> Add(ProductContext context, Product p)
        {
            await context.AddAsync(p);
            await context.SaveChangesAsync();

            return p;
        }

        public static async Task<Product> Edit(ProductContext context, Product p)
        {
            await context.AddAsync(p);
            context.Entry(p).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return p;
        }

        public static async Task<Product> GetProductById(ProductContext context, int id)
        {
            Product p =
                await (from product in context.Product
                       where product.ProductId == id
                       select product).SingleOrDefaultAsync();

            return p;
        }

        public static async Task Delete(ProductContext context, Product p)
        {
            await context.AddAsync(p);
            context.Entry(p).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
