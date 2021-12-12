using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HouseProjectWpfApi.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Repositories;
using DataAccesLayer;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer.EF;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.Enteties;
using Microsoft.AspNetCore.Identity;
using HouseProjectWpfApi.View;

namespace HouseProjectWpfApi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();
            IUserServices? userServices = serviceProvider.GetService<IUserServices>();
            IAdServices? adServices = serviceProvider.GetService<IAdServices>();
            IFavoritesServices? favoritesServices = serviceProvider.GetService<IFavoritesServices>();
            IForCompareServices? forCompareServices = serviceProvider.GetService<IForCompareServices>();
            IImageServices? imageServices = serviceProvider.GetService<IImageServices>();
            ICommentServices? commentServices = serviceProvider.GetService<ICommentServices>();
            Window loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel(userServices, adServices, favoritesServices, forCompareServices, commentServices, imageServices);
            loginWindow.Show();
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            #region Repositories
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IForCompareRepository, ForCompareRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Services
            services.AddTransient<IAdServices, AdServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IFavoritesServices, FavoritesServices>();
            services.AddTransient<IForCompareServices, ForcompareServices>();
            services.AddTransient<IImageServices, ImageServices>();
            #endregion

            services.AddDbContext<AppDBContext>(opts => opts.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true"));

            // For Mapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // For Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();

            services.AddLogging();
            return services.BuildServiceProvider();
        }
    }
}
