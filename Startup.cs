using Fbits.VueMpaTemplate.VueCliMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace Fbits.VueMpaTemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var razorPages = services.AddRazorPages();
            var mvc = services.AddControllersWithViews();


            // following is required to allow razor views and pages to auto-reload
            #if (DEBUG)
            mvc.AddRazorRuntimeCompilation();
            razorPages.AddRazorRuntimeCompilation();
            #endif

            services.AddLiveReload(config =>
            {
                // optional - use config instead
                //config.LiveReloadEnabled = true;
                //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
            });

            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            // Before any other output generating middleware handlers
            //app.UseLiveReload();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                //if (env.IsDevelopment())
                //{
                //    endpoints.MapToVueCliProxy(
                //        "{*path}",
                //        new SpaOptions {SourcePath = "ClientApp"},
                //        npmScript: "dev",
                //        regex: "Build complete");
                //}

                if (env.IsDevelopment())
                {
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions {SourcePath = "ClientApp"},
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }

                endpoints.MapRazorPages();
            });

            app.UseSpa(spa => { spa.Options.SourcePath = "ClientApp"; });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseVueDevelopmentServer(npmScript: "serve");
            //    }
            //});
        }
    }
}