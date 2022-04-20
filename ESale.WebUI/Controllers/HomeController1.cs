using ESale.Business.Abstract;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly IProductService _productService;

        public HomeController1(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetProductList()
            });
        }

    }
}
