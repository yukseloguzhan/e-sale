using ESale.Business.Abstract;
using ESale.Entities.Concrete;
using ESale.WebUI.Identity;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {

        private IProductService _productService;
        private ICategoryService _categoryService;
        private IImageFileService _imageFileService;
        private IHostingEnvironment Environment;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminsController(IProductService productService, ICategoryService categoryService, IImageFileService imageFileService, IHostingEnvironment environment, RoleManager<ApplicationRole> roleManager = null)
        {
            _productService = productService;
            _categoryService = categoryService;
            _imageFileService = imageFileService;
            Environment = environment;
            _roleManager = roleManager;
        }

        public IActionResult ProductList()
        {
            var list = new ProductListModel() { Products = _productService.GetProductList() };
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model, List<IFormFile> ChosenFiles)
        {

            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ImageURL = model.ImageURL
                };

                _productService.ProductAdd(entity);

                string path = ImagePath();
                int productId = entity.Id;
                foreach (IFormFile item in ChosenFiles)
                {
                    ImageFile photo = new ImageFile();

                    string _photoName = Path.GetFileName(item.FileName);
                    string photoName = DateTime.Now.ToString("yymmssfff") + _photoName;
                    using (FileStream stream = new FileStream(Path.Combine(path, photoName), FileMode.Create))
                    {
                        item.CopyTo(stream);
                        photo.ImagePath = photoName;
                        photo.ProductId = productId;
                        _imageFileService.ImageFileAdd(photo);
                        ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", photoName);
                    }
                }


                return RedirectToAction("ProductList");
            }

            return View(model);

        }



        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIDWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageURL = entity.ImageURL,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()

            };

            var imageList = _imageFileService.ImageFileListGetByProductId(entity.Id);
            ViewBag.imageList = imageList;
            ViewBag.AllCategories = _categoryService.GetCategoryList().ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, IFormFile ImageFile, List<IFormFile> ChosenFiles)
        {

            if (ModelState.IsValid)
            {
                var entity = _productService.GetByID(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;


                if (ImageFile != null)
                {

                    //var path = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot/ProductImages/" , ImageFile.FileName);
                    //using (var stream = new FileStream(path, FileMode.Create))  // akış  : dosyayı ilgili lokasyona oluşturmaya hazırım)
                    //{
                    //    await ImageFile.CopyToAsync(stream);
                    //}

                    var extension = Path.GetExtension(ImageFile.FileName);  // uzantı    // Ornek: resim.jpg yukledik extention=jpg
                    var imageName = Guid.NewGuid() + extension;   // yeni isim verdik
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", imageName);   // konum yol dosya yolu

                    using (var stream = new FileStream(location, FileMode.Create))  // akış  : dosyayı ilgili lokasyona oluşturmaya hazırım)
                    {
                        await ImageFile.CopyToAsync(stream);
                        entity.ImageURL = imageName;
                    }


                }

                string path = ImagePath();
                int productId = entity.Id;
                foreach (IFormFile item in ChosenFiles)
                {
                    ImageFile photo = new ImageFile();

                    string _photoName = Path.GetFileName(item.FileName);
                    string photoName = DateTime.Now.ToString("yymmssfff") + _photoName;
                    using (FileStream stream = new FileStream(Path.Combine(path, photoName), FileMode.Create))
                    {
                        item.CopyTo(stream);
                        photo.ImagePath = photoName;
                        photo.ProductId = productId;
                        _imageFileService.ImageFileAdd(photo);

                    }
                }



                _productService.ProductUpdate(entity, model.CategoryIds);

                return RedirectToAction("ProductList");
            }

            ViewBag.AllCategories = _categoryService.GetCategoryList().ToList();

            return View(model);

        }


        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetByID(productId);

            if (entity != null)
            {
                _productService.ProductDelete(entity);
            }

            return RedirectToAction("ProductList");
        }



        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetCategoryList()
            });
        }


        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };
            _categoryService.CategoryAdd(entity);

            return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIDWithProducts(id);

            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategories.Select(i => i.Product).ToList()
            });
        }


        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetByID(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.CategoryUpdate(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetByID(categoryId);

            if (entity != null)
            {
                _categoryService.CategoryDelete(entity);
            }

            return RedirectToAction("CategoryList");
        }



        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);

            return Redirect("/Admins/EditCategory/" + categoryId);
        }



        private string ImagePath()
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }



        public IActionResult RoleList()
        {
            var list = _roleManager.Roles.ToList();
            return View(list);
        }




    }





}
