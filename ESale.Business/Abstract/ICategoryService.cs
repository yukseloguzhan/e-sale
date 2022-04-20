using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetCategoryList();
        Category CategoryAdd(Category entity);
        Category GetByID(int id);
        Category GetByIDWithProducts(int id);

        void CategoryDelete(Category entity);
        Category CategoryUpdate(Category entity);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
