using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SportsStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace SportsStore
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
            services.AddDbContext<Models.ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddIdentity<ApplicationUser, IdentityRole>(opts => {
                opts .User.RequireUniqueEmail = true;
                opts .Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>() .AddDefaultTokenProviders();
            services.AddTransient<Models.IProductRepository, EFProductRepository>();
            services.AddScoped<Models.Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc(option=>
            {
                option.FormatterMappings.SetMediaTypeMappingForFormat("xml",new MediaTypeHeaderValue("application/xml"));
                option.RespectBrowserAcceptHeader = true;
                option.ReturnHttpNotAcceptable = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddXmlDataContractSerializerFormatters();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    } );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1 });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            ApplicationDbContext.CreateAdminAccount(app.ApplicationServices,Configuration).Wait();
            //SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
