using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        Category GetByIDWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
