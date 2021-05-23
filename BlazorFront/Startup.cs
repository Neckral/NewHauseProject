using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorFront.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Blazored.LocalStorage;
using BlazorFront.Validation;

namespace BlazorFront
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();

            #region ValidationServices
            services.AddScoped<UserValidator>();
            services.AddScoped<AdValidator>();
            services.AddScoped<CommentValidator>();
            services.AddScoped<AdEditValidator>();
            #endregion

            #region HttpClients
            services.AddHttpClient<IUserServices, UserServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/User/"); 
            });

            services.AddHttpClient<IAdServices, AdServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/Ad/");
            });

            services.AddHttpClient<IImageServices, ImageServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/Image/");
            });

            services.AddHttpClient<IFavoritesServices, FavoritesServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/Favorites/");
            });

            services.AddHttpClient<IForCompareServices, ForCompareServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/ForCompare/");
            });

            services.AddHttpClient<ICommentServices, CommentServices>(client => {
                client.BaseAddress = new Uri("https://localhost:44365/Comment/");
            });
            #endregion

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
