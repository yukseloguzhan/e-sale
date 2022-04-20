using ESale.Business.Abstract;
using ESale.WebUI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class CartProductsCountViewComponent  : ViewComponent
    {

        private ICartService _cartService;
        private IFavoryBoxService _favoryBoxService;
        private UserManager<ApplicationUser> _userManager;   // 

        public CartProductsCountViewComponent(ICartService cartService, UserManager<ApplicationUser> userManager, IFavoryBoxService favoryBoxService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _favoryBoxService = favoryBoxService;
        }

        public IViewComponentResult Invoke()
        {

            // SONRA AÇ BURAYI
            //var cart = _cartService.GetCartByUserId(_userManager.GetUserId(HttpContext.User));
            //var favoryBox = _favoryBoxService.GetFavoryBoxByUserId(_userManager.GetUserId(HttpContext.User));
            //if (cart == null)
            //{
            //    return View(0);
            //}
            //var productsCount = _cartService.ProductCount(cart.UserId);
            //var favoryListCount = _favoryBoxService.FavoryCount(favoryBox.UserId);
            //ViewBag.favoryListCount = favoryListCount;
            //return View(productsCount);
            return View(5);
        }
    }
}
