using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetProductList();
        List<Product> GetProductsByCategory(string categoryname,int page,int pageSize);
        List<Product> GetProductsByCategory(string categoryname);

        Product ProductAdd(Product entity);
        Product GetByID(int id);
        Product GetProductDetails(int id);

        void ProductDelete(Product entity);
        Product ProductUpdate(Product entity);
        int GetCountByCategory(string categoryname);
        Product GetByIDWithCategories(int id);
        void ProductUpdate(Product entity,int[] categoryIds );

        List<Product> SearchForProducts(string productName);

    }
}
