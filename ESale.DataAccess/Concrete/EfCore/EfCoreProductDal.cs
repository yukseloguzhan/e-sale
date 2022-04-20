using ESale.DataAccess.Abstract;
using ESale.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public int CountByCategory(string categoryname)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!String.IsNullOrEmpty(categoryname))
                {
                    products = products.Include(i => i.ProductCategories)
                       .ThenInclude(i => i.Category)
                       .Where(i => i.ProductCategories.Any(x => x.Category.Name.ToLower() == categoryname.ToLower()));
                }

                return products.Count();

            }

        }

        public Product GetByIDWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();

            }
        }

        public Product ProductDetails(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }

        }

        public List<Product> ProductsByCategory(string category,int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!String.IsNullOrEmpty(category))
                {
                    products = products.Include(i => i.ProductCategories)
                       .ThenInclude(i => i.Category)
                       .Where(i => i.ProductCategories.Any(x => x.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            }
        }

        public List<Product> ProductsByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!String.IsNullOrEmpty(category))
                {
                    products = products.Include(i => i.ProductCategories)
                       .ThenInclude(i => i.Category)
                       .Where(i => i.ProductCategories.Any(x => x.Category.Name.ToLower() == category.ToLower()));
                }

                return products.ToList();

            }
        }

        public void ProductUpdate(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.Id == entity.Id);


                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImageURL = entity.ImageURL;
                    product.Price = entity.Price;

                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }



            }
        }

        public List<Product> SearchForProducts(string productName)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!String.IsNullOrEmpty(productName))
                {
                    products = products.Include(i => i.ProductCategories)
                       .ThenInclude(i => i.Category)
                       //.Where(i => i.ProductCategories.Any(x => x.Product.Name.ToLower() == productName.ToLower()));
                       .Where(i=>i.Name.ToLower().Contains(productName.ToLower()));
                }

                return products.ToList();

            }
        }
    }
}
