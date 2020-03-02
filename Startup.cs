using Fbits.VueMpaMiddleware;
using Fbits.VueMpaTemplate.Configuration;
using Fbits.VueMpaTemplate.Enums;
using Fbits.VueMpaTemplate.Helpers.Extensions;
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
        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            Config = config;
            Env = env;
            ReloadType = config["RELOAD"]?.TryToParseEnum<ReloadTypes>() ?? ReloadTypes.None;
        }

        public IConfiguration Config { get; }
        public IWebHostEnvironment Env { get; }
        public ReloadTypes ReloadType { get; set; }
        public bool IsUseLiveReload => ReloadType == ReloadTypes.LiveReload;
        public bool IsUseHotModuleReload => ReloadType == ReloadTypes.HotModuleReload;

        public void ConfigureServices(IServiceCollection services)
        {
            App.Services = services;
            App.OnApplicationStart(Env, Config);

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
            if (IsUseLiveReload) app.UseLiveReload();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                if (env.IsDevelopment() && IsUseLiveReload)
                {
                    // this will automatically run "npm run dev" which will build client app and watch for changes
                    // all web calls wil go to localhost:5001
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "ClientApp" },
                        npmScript: "dev",
                        regex: "Build complete");
                }

                if (env.IsDevelopment() && IsUseHotModuleReload)
                {
                    // this will automatically run "npm run serve" which will build client app and run dev server
                    // all web calls will go to dev server localhost:8080 which will proxy asp.net url's to localhost:5001
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions {SourcePath = "ClientApp"},
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }

                endpoints.MapRazorPages();
                app.UseSpa(spa => { spa.Options.SourcePath = "ClientApp"; });
            });
        }
    }
}