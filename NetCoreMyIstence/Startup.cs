using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Repository.SqlServer;
using NetCoreMyIstence.Services;

namespace NetCoreMyIstence
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //setSess取不到值Seesionid改变 将checkconsetneeded默认ture 改为 false  
                //默认CheckConsentNeeded =true;GDPR规定了cookie的操作方式，并且在用户同意使用cookie之前不会使用。
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddTransient<DbContext>();
            //注册服务
            services.AddSingleton<IBannerServices, BannerServices>();
            services.AddSingleton<INewsServices, NewsServices>();
            services.AddSingleton<INewsClassifyServices, NewsClassifyServices>();
            services.AddSingleton<INewsCommentServices, NewsCommentServices>();

            services.AddSingleton<IBannerRepository, BannerRepository>();
            services.AddSingleton<INewsRepository, NewsRepository>();
            services.AddSingleton<INewsClassifyRepository, NewsClassifyRepository>();
            services.AddSingleton<INewsCommentRepsitory, NewsCommentRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //注册Session
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //注册Session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
