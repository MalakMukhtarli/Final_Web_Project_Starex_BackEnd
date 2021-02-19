using Buisness.Abstract;
using Buisness.Concret;
using DataAccess.Abstract;
using DataAccess.Concret;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddScoped<IAboutDAL, EFAboutDal>();
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<INewsDal, EfNewsDal>();
            services.AddScoped<INewsService, NewsManager>();
            services.AddScoped<INewsDetailDal, EFNewsDetailDal>();
            services.AddScoped<INewsDetailService, NewsDetailManager>();
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
            //services.AddScoped<IStoreDal, EFStoreDal>();
            //services.AddScoped<IStoreService, StoreManager>();
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
            //services.AddScoped<IOrderService, OrderManager>();
            //services.AddScoped<IOrderDal, EFOrderDal>();
            //services.AddScoped<INotficationService, NotficationManager>();
            //services.AddScoped<INotficationDal, EFNotficationDal>();
            //services.AddScoped<IAddressService, AddressManager>();
            //services.AddScoped<IAddressDal, EFAddressDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
