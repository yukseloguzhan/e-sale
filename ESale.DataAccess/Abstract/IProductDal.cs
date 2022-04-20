using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Product ProductDetails(int id);
        List<Product> ProductsByCategory(string category,int page,int pageSize);
        List<Product> ProductsByCategory(string category);
        int CountByCategory(string categoryname);
        Product GetByIDWithCategories(int id);
        void ProductUpdate(Product entity, int[] categoryIds);
        List<Product> SearchForProducts(string productName);
    }
}
