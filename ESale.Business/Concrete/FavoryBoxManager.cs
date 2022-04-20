using ESale.Business.Abstract;
using ESale.DataAccess.Abstract;
using ESale.DataAccess.Concrete.EfCore;
using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Concrete
{
    public class FavoryBoxManager : IFavoryBoxService
    {

        private IFavoryBoxDal _favoryBoxDal;

        public FavoryBoxManager(IFavoryBoxDal favoryBoxDal)
        {
            _favoryBoxDal = favoryBoxDal;
        }

        public void AddtoFavoryBox(string userId, int productId)
        {
            var favoryBox = _favoryBoxDal.GetByUserId(userId);

            if (favoryBox != null)
            {
                var favoryProduct = favoryBox.FavoryItems.FirstOrDefault(x => x.ProductId == productId);
                if (favoryProduct == null)
                {
                    favoryBox.FavoryItems.Add(new FavoryItem()
                    {
                        ProductId = productId,
                        FavoryBoxId = favoryBox.Id
                    });

                    _favoryBoxDal.Update(favoryBox);


                }
                else
                {
                    _favoryBoxDal.DeleteFromFavoryBox(favoryBox.Id, productId);

                }

              

            }



        }

        public void CreateFavoryBox(string userId)
        {
            _favoryBoxDal.Add(new FavoryBox() { UserId = userId});
        }

        public void DeleteFromFavoryBox(string userId, int productId)
        {
            var favoryBox = GetFavoryBoxByUserId(userId);
            if (favoryBox != null)
            {

                _favoryBoxDal.DeleteFromFavoryBox(favoryBox.Id, productId);


            }
        }

        public int FavoryCount(string userId)
        {
            var list = GetFavoryBoxByUserId(userId);
            if (list != null)
            {

                return list.FavoryItems.Count();
            }

            return 0;
        }

        public FavoryBox GetFavoryBoxByUserId(string userId)
        {
            return _favoryBoxDal.GetByUserId(userId);

        }
    }
}
