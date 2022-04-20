
using ESale.Business.Abstract;
using ESale.WebUI.Identity;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Controllers
{
    public class ShopsController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IFavoryBoxService _favoryBoxService;
        private UserManager<ApplicationUser> _userManager;   // 

        public ShopsController(IProductService productService, ICategoryService categoryService, IFavoryBoxService favoryBoxService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _favoryBoxService = favoryBoxService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(string categoryname, int page = 1)
        {
            const int pageSize = 6;

            ViewBag.categoryList = _categoryService.GetCategoryList().Take(6);


            return View(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(categoryname),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = categoryname

                },
                Products = _productService.GetProductsByCategory(categoryname, page, pageSize)
            });
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var product = _productService.GetProductDetails((int)id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.categoryList = _categoryService.GetCategoryList().Take(6);
            ViewBag.SameCategoryProducts = _productService.GetProductsByCategory(product.ProductCategories.ElementAt(0).Category.Name).Take(4);


            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }


        public IActionResult SearchList(string productName)
        {

            var list = _productService.SearchForProducts(productName);
            ViewBag.productName = productName;
            return View(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = list.Count,
                    CurrentPage = 1,
                    ItemsPerPage = 6,
                    CurrentCategory = ""

                },
                Products = list
            });
        }


        [HttpGet]
        public IActionResult AddtoFavoryBox(int id)
        {
            _favoryBoxService.AddtoFavoryBox(_userManager.GetUserId(User), id);
            return Ok("aaaaaaa");
            
        }

        //[HttpPost]
        //public IActionResult DeleteFromCart(int productId)
        //{
        //    _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        public IActionResult FilterProducts(FilterModel model)
        {

            int sortParameter = model.SortParameter;
            string price = model.Price;
            string title = model.Title;

            var list = _productService.GetProductList();
            ViewBag.categoryList = _categoryService.GetCategoryList().Take(6);

            if (model.Title != null)
            {
                list = _productService.SearchForProducts(model.Title);

            }

            if (sortParameter == 1)
            {
                list = list.OrderByDescending(x => x.CreateDate).ToList();
            }
            if (sortParameter == 2)
            {
                list = list.OrderByDescending(x => x.Price).ToList();
            }
            if (sortParameter == 3)
            {
                list = list.OrderBy(x => x.Price).ToList();
            }


            if (price != null)
            {
                string[] range = price.Split("-");
                list = list.Where(x => x.Price > int.Parse(range[0]) && x.Price < int.Parse(range[1])).ToList();
            }




            var productsList = new ProductListModel()
            {
                Products = list,
                PageInfo = new PageInfo()
                {
                    TotalItems = list.Count,
                    CurrentPage = 1,
                    ItemsPerPage = 6,
                    CurrentCategory = ""

                }
            };



            return View(productsList);


        }



    }
}
