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
    public class ImageFileManager : IImageFileService
    {
        readonly IImageFileDal _imageFileDal;

        public ImageFileManager(IImageFileDal imageFileDal)
        {
            _imageFileDal = imageFileDal;
        }

        public List<ImageFile> GetImageFileList()
        {
            return _imageFileDal.List();
        }

        public ImageFile ImageFileAdd(ImageFile entity)
        {
            return _imageFileDal.Add(entity);
        }

        public ImageFile GetByID(int id)
        {
            return _imageFileDal.Get(x => x.ImageFileId == id);
        }

        public void ImageFileDelete(ImageFile entity)
        {
            _imageFileDal.Delete(entity);
        }

        public ImageFile ImageFileUpdate(ImageFile entity)
        {
            return _imageFileDal.Update(entity);
        }

        public List<ImageFile> ImageFileListGetByProductId(int productId)
        {
            return _imageFileDal.List(x => x.ProductId == productId);
        }
    }
}
