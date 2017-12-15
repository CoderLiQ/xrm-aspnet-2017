using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

using xrm_aspnet_2017.Data;
using Microsoft.EntityFrameworkCore;
using xrm_aspnet_2017.Services;

namespace xrm_aspnet_2017 {
    public class MyMiddleware {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next) {

            _next = next;
        }

        public async Task Invoke(HttpContext context) {

            // await context.Response.WriteAsync("\nClass-MiddleWare starts logging...");

            var sw = new Stopwatch();
            sw.Start();

            await _next.Invoke(context);

            sw.Stop();
            await context.Response.WriteAsync(String.Format("<br>Elapsed Time - {0} ms. (Class-MiddleWare)"
                    , sw.ElapsedMilliseconds));


        }
    }

    public class Startup {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("conf.json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IConfiguration Configuration { get; }
        //public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddDbContext<UniversityContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStudentManager>(manager => new StudentManager());

            services.AddMvc();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {

            #region logging loading time using middlewares (lab2)
            //app.UseMiddleware<MyMiddleware>();

            //app.Use(async (context, next) => {

            //await context.Response.WriteAsync("\nLambda-MiddleWare starts logging...");

            //var sw = new Stopwatch();
            //sw.Start();                

            //await next.Invoke();

            //sw.Stop();
            //await context.Response.WriteAsync(String.Format("<br>Elapsed Time - {0} ms. (Lambda-MiddleWare)"
            //    , sw.ElapsedMilliseconds));
            //});
            #endregion


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
