using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.DataAccess.Abstract
{
    public interface IFavoryBoxDal : IEntityRepository<FavoryBox>
    {
        FavoryBox GetByUserId(string userId);
        void DeleteFromFavoryBox(int favoryBoxId, int productId);

        //void ClearFavoryBox(string favoryBoxId);

    }
}
