using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Business.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetCommentList();
        List<Comment> GetCommentList(int id);
        Comment CommentAdd(Comment entity);
        void CommentDelete(Comment entity);
        Comment GetByID(int id);
        Comment CommentUpdate(Comment entity);
    }
}
