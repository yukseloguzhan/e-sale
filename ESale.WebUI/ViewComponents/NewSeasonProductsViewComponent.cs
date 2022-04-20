using ESale.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class NewSeasonProductsViewComponent : ViewComponent
    {

        private IProductService _productService;

        public NewSeasonProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _productService.GetProductList().OrderByDescending(x=>x.CreateDate).Take(8);
            return View(list);
        }

    }
}
