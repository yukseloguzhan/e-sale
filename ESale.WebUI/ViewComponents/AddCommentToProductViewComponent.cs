using ESale.Business.Abstract;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class AddCommentToProductViewComponent   :  ViewComponent
    {

        private ICommentService _commentService;

        public AddCommentToProductViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var comment = new CommentModel();
            comment.UserName = User.Identity.Name;
            comment.ProductId = id;
            return View(comment);

        }





    }
}
