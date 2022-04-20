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



            // services.AddTransient<IEmailSender,MyEmailSender>();// email onaylamak i�in


            services.AddDbContext<ApplicationIdentityDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()     // nerede depolans�n datalar
                .AddDefaultTokenProviders();   // Parolas�n�,mail'�n� falan resetlerken benzersiz anahtar vermek i�in bilet numaras� gibi



            services.Configure<IdentityOptions>(options =>
            {
                // password i�in ayarlar
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;


                options.Lockout.MaxFailedAccessAttempts = 5;  // 5 defa yanl�� girme hakk� var
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   // 5 dk bekler 5 hatadan sonra
                options.Lockout.AllowedForNewUsers = true; // yeni kullan�c� i�inde ge�erli


                // Kullan�c� i�in ayarlar
                options.User.RequireUniqueEmail = true;   // daha �nce bu mail kay�tl�ysa hata verir
                options.SignIn.RequireConfirmedEmail = false;   // kay�t olunca e-mail ile do�rulans�n



            });


            // Login olunca server da session olu�cak Requestler aras� Session'� tan�ycak cookie 
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
                options.LogoutPath = "/Accounts/Logout";
                options.AccessDeniedPath = "/Accounts/AccessDenied";    // yetkisi olmayan yere girerse
                options.SlidingExpiration = true;     // Mesela cookie 20 dk kullan�c� 15.dk da talep g�nderdi�inde tekrar 20dk ba�lar
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);   // 1 g�n cookie taray�c� da saklan�r
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true, //  Taray�c� �zerinde �al��an scriptler de ula�ablir false yaparsak
                    SameSite = SameSiteMode.Strict   // Ba�ka biri bizim cookie mizi al�p servera istekde bulunamaz sadece bizim taray�c�m�zda sakl�
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
            //app.CustomStaticFiles();  // Biz ekledik dosyalar� d��ar� a�abilmek i�in



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
