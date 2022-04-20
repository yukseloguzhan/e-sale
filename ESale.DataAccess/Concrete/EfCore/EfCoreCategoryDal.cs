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
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM ProductCategory WHERE ProductId = @p0  AND  CategoryId = @p1 ";
                context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
            }
        }

        public Category GetByIDWithProducts(int id)
        {
            using (var context = new ShopContext())
            {
               return context.Categories.Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault();
            }
        }
    }
}
