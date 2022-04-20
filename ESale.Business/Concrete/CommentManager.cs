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
    public class CommentManager : ICommentService
    {

        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public Comment CommentAdd(Comment entity)
        {
            return _commentDal.Add(entity);

        }

        public void CommentDelete(Comment entity)
        {
             _commentDal.Delete(entity);
        }

        public Comment CommentUpdate(Comment entity)
        {
            return _commentDal.Update(entity);
        }

        public Comment GetByID(int id)
        {
            return _commentDal.Get(x=>x.Id == id);
        }

        public List<Comment> GetCommentList()
        {
            return _commentDal.List();

        }

        public List<Comment> GetCommentList(int id)
        {
            return _commentDal.List(x=>x.ProductId == id);

        }
    }
}
