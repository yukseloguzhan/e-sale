using ESale.Business.Abstract;
using ESale.Business.Concrete;
using ESale.DataAccess.Abstract;
using ESale.DataAccess.Concrete.EfCore;
using ESale.DataAccess.Concrete.EfCore.SeedDatabase;
using ESale.WebUI.EmailServices;
using ESale.WebUI.Identity;
using ESale.WebUI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductDal, EfCoreProductDal>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<ICartService, CartManager>();

            services.AddScoped<IOrderDal, EfCoreOrderDal>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddScoped<ICommentDal, EfCoreCommentDal>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IFavoryBoxDal, EfCoreFavoryBoxDal>();
            services.AddScoped<IFavoryBoxService, FavoryBoxManager>();

            services.AddScoped<IImageFileDal, EfCoreImageFileDal>();
            services.AddScoped<IImageFileService, ImageFileManager>();



            // services.AddTransient<IEmailSender,MyEmailSender>();// email onaylamak için


            services.AddDbContext<ApplicationIdentityDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()     // nerede depolansýn datalar
                .AddDefaultTokenProviders();   // Parolasýný,mail'ýný falan resetlerken benzersiz anahtar vermek için bilet numarasý gibi



            services.Configure<IdentityOptions>(options =>
            {
                // password için ayarlar
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;


                options.Lockout.MaxFailedAccessAttempts = 5;  // 5 defa yanlýþ girme hakký var
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   // 5 dk bekler 5 hatadan sonra
                options.Lockout.AllowedForNewUsers = true; // yeni kullanýcý içinde geçerli


                // Kullanýcý için ayarlar
                options.User.RequireUniqueEmail = true;   // daha önce bu mail kayýtlýysa hata verir
                options.SignIn.RequireConfirmedEmail = false;   // kayýt olunca e-mail ile doðrulansýn



            });


            // Login olunca server da session oluþcak Requestler arasý Session'ý tanýycak cookie 
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
                options.LogoutPath = "/Accounts/Logout";
                options.AccessDeniedPath = "/Accounts/AccessDenied";    // yetkisi olmayan yere girerse
                options.SlidingExpiration = true;     // Mesela cookie 20 dk kullanýcý 15.dk da talep gönderdiðinde tekrar 20dk baþlar
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);   // 1 gün cookie tarayýcý da saklanýr
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true, //  Tarayýcý üzerinde çalýþan scriptler de ulaþablir false yaparsak
                    SameSite = SameSiteMode.Strict   // Baþka biri bizim cookie mizi alýp servera istekde bulunamaz sadece bizim tarayýcýmýzda saklý
                };

            });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();   // Ekledik
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseAuthentication();  // ekledik
            app.UseStaticFiles();
            //app.CustomStaticFiles();  // Biz ekledik dosyalarý dýþarý açabilmek için



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "Products/{categoryname?}",
                    defaults: new { controller = "Shops", action = "List" }
                    );



                endpoints.MapControllerRoute(
                    name: "AdminProducts",
                    pattern: "Admin/Products",
                    defaults: new { controller = "Admins", action = "ProductList" }
                    );

                endpoints.MapControllerRoute(
                   name: "AdminProducts",
                   pattern: "Admin/Products/{id?}",
                   defaults: new { controller = "Admins", action = "EditProduct" }
                   );

                endpoints.MapControllerRoute(
                    name: "Cart",
                    pattern: "Cart",
                    defaults: new { controller = "Carts", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                   name: "CheckOut",
                   pattern: "CheckOut",
                   defaults: new { controller = "Carts", action = "CheckOut" }
                   );


                endpoints.MapControllerRoute(
                   name: "Orders",
                   pattern: "Orders",
                   defaults: new { controller = "Carts", action = "GetOrders" }
                   );


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index2}/{id?}");











            });
        }
    }
}
