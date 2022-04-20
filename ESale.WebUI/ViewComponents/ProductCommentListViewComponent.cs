using ESale.Business.Abstract;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ESale.WebUI.ViewComponents
{
    public class ProductCommentListViewComponent : ViewComponent
    {

        private ICommentService _commentService;

        public ProductCommentListViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var list = _commentService.GetCommentList(id);
            var commentListModel = new CommentListModel()
            {
                Comments = list
            };
            return View(commentListModel);

        }





    }
}
