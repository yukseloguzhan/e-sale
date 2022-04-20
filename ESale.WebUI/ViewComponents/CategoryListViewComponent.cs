using ESale.Business.Abstract;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class CategoryListViewComponent :  ViewComponent
    {

        private ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var list = new CategoryListViewModel() {  
                Categories = _categoryService.GetCategoryList(),
                SelectedCategory = RouteData.Values["categoryname"]?.ToString()  
            };

            return View(list);
        }
    }
}
