using ESale.Business.Abstract;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class FilterProductsViewComponent   : ViewComponent
    {
        private IProductService _productService;

        public FilterProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new FilterModel();
            return View(model);
        }
    }
}
