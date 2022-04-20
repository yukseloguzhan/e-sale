using ESale.Business.Abstract;
using ESale.Entities.Concrete;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Controllers
{
    public class CommentsController : Controller
    {

        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult List()
        {
            return View();
        }


        [HttpGet]
        public PartialViewResult CreateComment(int id)
        {
            var comment = new CommentModel();
            comment.UserName = User.Identity.Name;
            ViewBag.productId = id;
            return PartialView(new CommentModel());
        }

        [HttpPost]
        public IActionResult CreateComment(CommentModel comment)
        {

            if (ModelState.IsValid)
            {
                var entity = new Comment()
                {
                    
                    CreateDate = DateTime.Now,
                    Description = comment.Description,
                    UserName = comment.UserName,
                    ProductId = comment.ProductId
                    
                };

                _commentService.CommentAdd(entity);
                return RedirectToAction("Details", "Shops",new { id = comment.ProductId  });

            }


            return View(comment);

           
        }




    }
}
