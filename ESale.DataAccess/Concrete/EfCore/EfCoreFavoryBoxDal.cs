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
    public class EfCoreFavoryBoxDal : EfCoreGenericRepository<FavoryBox, ShopContext>, IFavoryBoxDal
    {
        //public void ClearFavoryBox(string favoryBoxId)
        //{
        //    throw new NotImplementedException();
        //}

        public void DeleteFromFavoryBox(int favoryBoxId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM FavoryItem WHERE FavoryBoxId = @p0  AND   ProductId = @p1";
                context.Database.ExecuteSqlRaw(cmd, favoryBoxId, productId);
            }
        }

        public FavoryBox GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.FavoryBoxes.Where(x => x.UserId == userId)
                     .Include(x => x.FavoryItems)
                     .ThenInclude(x => x.Product)
                     .FirstOrDefault();


            }
        }


        public override FavoryBox Update(FavoryBox entity)
        {

            using (var context = new ShopContext())
            {
                context.FavoryBoxes.Update(entity);  // ilişkili alt tabloları da günceller
                context.SaveChanges();
            }

            return entity;

        }

    }
}
