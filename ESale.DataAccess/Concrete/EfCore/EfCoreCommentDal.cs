using ESale.DataAccess.Abstract;
using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Concrete.EfCore
{
    public class EfCoreCommentDal : EfCoreGenericRepository<Comment, ShopContext>, ICommentDal
    {

    }
}
