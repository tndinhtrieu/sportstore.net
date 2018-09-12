using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;

        public Product DeleteProduct(int productID)
        {
            Product _product = context.Products.SingleOrDefault(x => x.ProductID == productID);
            if (_product != null)
            {
                context.Products.Remove(_product);
                context.SaveChanges();
            }
            return _product;
        }

        public void SaveProduct(Product product)
        {

            if (product.ProductID == 0)
                context.Products.Add(product);
            else
            {
                Product _product = context.Products.SingleOrDefault(x => x.ProductID == product.ProductID);
                if (_product != null)
                {
                    _product.SetObject(product);
                }
            }
            context.SaveChanges();
        }

       
    }
}
