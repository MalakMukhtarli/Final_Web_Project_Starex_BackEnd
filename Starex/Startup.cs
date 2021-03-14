using Buisness.Abstract;
using Buisness.Concret;
using DataAccess.Abstract;
using DataAccess.Concret;
using Entity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Starex.Identity;
using System;
using System.Text;

namespace Starex
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
            services.AddControllers();

            #region AddScoped
            services.AddScoped<IAboutDAL, EFAboutDal>();
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<INewsDal, EfNewsDal>();
            services.AddScoped<INewsService, NewsManager>();
            services.AddScoped<IQuestionNavbarDal, EFQuestionNavbarDal>();
            services.AddScoped<IQuestionNavbarService, QuestionNavbarManager>();
            services.AddScoped<IQuestionDal, EFQuestionDal>();
            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IIntroDal, EFIntroDal>();
            services.AddScoped<IIntroService, IntroManager>();
            services.AddScoped<IHowWorksDal, EFHowWorksDal>();
            services.AddScoped<IHowWorksService, HowWorksManager>();
            services.AddScoped<IAdvantagesDal, EFAdvantagesDal>();
            services.AddScoped<IAdvantagesService, AdvantagesManager>();
            services.AddScoped<IStoreDal, EFStoreDal>();
            services.AddScoped<IStoreService, StoreManager>();
            services.AddScoped<IServiceService, ServiceManager>();
            services.AddScoped<IServiceDal, EFServiceDal>();
            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<ICountryDal, EFCountryDal>();
            services.AddScoped<ICountryContactService, CountryContactManager>();
            services.AddScoped<ICountryContactDal, EFCountryContactDal>();
            services.AddScoped<ITariffService, TariffManager>();
            services.AddScoped<ITariffDal, EFTariffDal>();
            services.AddScoped<IBranchContactService, BranchContactManager>();
            services.AddScoped<IBranchContactDal, EFBranchContactDal>();
            services.AddScoped<IBranchService, BranchManager>();
            services.AddScoped<IBranchDal, EFBranchDal>();
            services.AddScoped<ICityService, CityManager>();
            services.AddScoped<ICityDal, EFCityDal>();
            services.AddScoped<IDeclarationService, DeclarationManager>();
            services.AddScoped<IDeclarationDal, EFDeclarationDal>();
            services.AddScoped<IDistrictTariffService, DistrictTariffManager>();
            services.AddScoped<IDistrictTariffDal, EFDistrictTariffDal>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDal, EFOrderDal>();
            services.AddScoped<INotficationService, NotficationManager>();
            services.AddScoped<INotficationDal, EFNotficationDal>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddScoped<IAddressDal, EFAddressDal>();
            services.AddScoped<IBalanceService, BalanceManager>();
            services.AddScoped<IBalanceDal, EFBalanceDal>();
            //services.AddScoped<IAppUserService, AppUserManager>();
            //services.AddScoped<IAppUserDal, EFAppUserDal>();
            #endregion

            services.AddDbContext<MyIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]);
            });

            services.AddIdentity<AppUser, IdentityRole>(IdentityOption =>
            {
                IdentityOption.Password.RequiredLength = 8;
                IdentityOption.Password.RequireDigit = true;
                IdentityOption.Password.RequireUppercase = false;
                IdentityOption.Password.RequireLowercase = true;
                IdentityOption.Password.RequireNonAlphanumeric = false;

                IdentityOption.User.RequireUniqueEmail = true;

                IdentityOption.Lockout.MaxFailedAccessAttempts = 5;  //nece defe sehv yazandan sonra block etsin
                IdentityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); 
                IdentityOption.Lockout.AllowedForNewUsers = true; //yeni qeydiyyatdan kecibse icaze versin sehv yazmaga
            }).AddEntityFrameworkStores<MyIdentityDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                options => options.WithOrigins("http://localhost:4030").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
            );
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
