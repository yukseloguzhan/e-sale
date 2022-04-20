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
    public class CategoryManager : ICategoryService
    {
        readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetCategoryList()
        {
            return _categoryDal.List();
        }

        public Category CategoryAdd(Category entity)
        {
            return _categoryDal.Add(entity);
        }

        public Category GetByID(int id)
        {
            return _categoryDal.Get(x => x.Id == id);
        }

        public void CategoryDelete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public Category CategoryUpdate(Category entity)
        {
            return _categoryDal.Update(entity);
        }

        public Category GetByIDWithProducts(int id)
        {
            return _categoryDal.GetByIDWithProducts(id);
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            _categoryDal.DeleteFromCategory(categoryId, productId);
        }
    }
}
