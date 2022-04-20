using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface IImageFileService
    {
        List<ImageFile> GetImageFileList();
        ImageFile ImageFileAdd(ImageFile entity);
        ImageFile GetByID(int id);

        void ImageFileDelete(ImageFile entity);
        ImageFile ImageFileUpdate(ImageFile entity);
        List<ImageFile> ImageFileListGetByProductId(int productId);
    }
}
