using ESale.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class CheapProductsViewComponent : ViewComponent
    {

        private IProductService _productService;

        public CheapProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var list = from x in _productService.GetProductList()
                       orderby x.Price ascending
                       select x;
            return View(list);
        }


    }
}
