using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DataAccesLayer.Repositories;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.EF;
using BuisnesLogicLayer.DTO;
using Microsoft.AspNetCore.Identity;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace HouseProjectWpf
{
    public class Startup
    {
        //private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
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

            #region Validators
            services.AddTransient<IValidator<AdCreateDTO>, AdValidator>();
            services.AddTransient<IValidator<AdEditDTO>, AdEditValidator>();
            services.AddTransient<IValidator<UserRegisterDTO>, UserValidator>();
            services.AddTransient<IValidator<CommentCreateDTO>, CommentValidator>();
            #endregion

            services.AddDbContext<AppDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:connectDB"]));

            services.AddControllers();
                     
            // For Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // For Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IUserServices userServices)
        {

            if (env.IsDevelopment())
            {
                // то выводим информацию об ошибке, при наличии ошибки
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //// добавляем возможности маршрутизации
            app.UseRouting();

            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();
        }
    }
}
