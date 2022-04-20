using ESale.Business.Abstract;
using ESale.DataAccess.Abstract;
using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product GetByID(int id)
        {
            return _productDal.Get(x => x.Id == id);
        }

        public Product GetByIDWithCategories(int id)
        {
            return _productDal.GetByIDWithCategories(id);
        }

        public int GetCountByCategory(string categoryname)
        {
            return _productDal.CountByCategory(categoryname);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.ProductDetails(id);
        }

        public List<Product> GetProductList()
        {
            return _productDal.List();
        }

        public List<Product> GetProductsByCategory(string categoryname,int page, int pageSize)
        {
            return _productDal.ProductsByCategory(categoryname,page, pageSize);
        }

        public List<Product> GetProductsByCategory(string categoryname)
        {
            return _productDal.ProductsByCategory(categoryname);
        }

        public Product ProductAdd(Product entity)
        {
            return _productDal.Add(entity);
        }

        public void ProductDelete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public Product ProductUpdate(Product entity)
        {
            return _productDal.Update(entity);
        }

        public void ProductUpdate(Product entity,int[] categoryIds)
        {
             _productDal.ProductUpdate(entity,categoryIds);
        }

        public List<Product> SearchForProducts(string productName)
        {
            return _productDal.SearchForProducts(productName);

        }
    }
}
