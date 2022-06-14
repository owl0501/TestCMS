using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCMS.Entity.Entity;
using TestCMS.Business.Abstract;
using TestCMS.Business.Concrete;
using TestCMS.DataAccess.Concrete;
using TestCMS.DataAccess.Abstract;


namespace TsetCMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Category Service
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGeneralRepo<CategoryTable>, GeneralRepo<CategoryTable>>();
            #endregion
            #region Product Service
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IGeneralRepo<ProductTable>, GeneralRepo<ProductTable>>();
            #endregion

            #region Cart Service
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IGeneralRepo<CartTable>, GeneralRepo<CartTable>>();
            #endregion

            services.AddControllersWithViews();
            //Register dbcontext
            services.AddDbContext<CMSDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CMSDBContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=ProductQueryResult}/{id?}");
            });
        }
    }
}
