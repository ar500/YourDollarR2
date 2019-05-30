using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YourDollarR2.Core;
using YourDollarR2.Core.Services;
using YourDollarR2.DataAccess;
using YourDollarR2.Dtos;

namespace YourDollarR2
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<YourDollarContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("YourDollarApp")));

            services.AddHttpContextAccessor();

            services.AddTransient<IFundsInCategoryService, FundsInCategoryService>();
            services.AddTransient<ICalculateBudgetFundsService, CalculateBudgetFundsService>();
            services.AddTransient<IExpenseService, ExpenseService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.AddEventSourceLogger();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IFundsInCategoryService catService, ICalculateBudgetFundsService fundsService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseNodeModules(env);
            app.UseCookiePolicy();

            app.UseAuthentication();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BudgetDto, Budget>();

                cfg.CreateMap<BudgetDto, Budget>().ReverseMap()
                    .BeforeMap((s, d) => d.UnAllocatedFunds = fundsService.CalculateUnallocateFunds(d.AllocatedFunds, s.AllottedFunds))
                    .BeforeMap((s, d) => d.AllocatedFunds = fundsService.CalculateAllocatedFunds(d.CategoryGroups))
                    .BeforeMap((s, d) => d.CategoryGroups = catService.GroupExpensesByCat(s.Bills));

                cfg.CreateMap<BudgetForCreateOrEditDto, Budget>();

                cfg.CreateMap<BudgetForCreateOrEditDto, Budget>().ReverseMap();

                cfg.CreateMap<BudgetCategoryDto, BudgetCategory>();

                cfg.CreateMap<BudgetCategoryDto, BudgetCategory>().ReverseMap();

                cfg.CreateMap<BudgetCategoryForSelectListDto, BudgetCategory>();

                cfg.CreateMap<BudgetCategoryForSelectListDto, BudgetCategory>().ReverseMap();

                cfg.CreateMap<BillDto, Bill>()
                    .IncludeBase<ExpenseBaseDto, ExpenseBase>();

                cfg.CreateMap<BillDto, Bill>()
                    .IncludeBase<ExpenseBaseDto, ExpenseBase>()
                    .ReverseMap();

                cfg.CreateMap<ExpenseBaseDto, ExpenseBase>();

                cfg.CreateMap<ExpenseBaseDto, ExpenseBase>().ReverseMap();

                cfg.CreateMap<BillForCreateDto, Bill>();

                cfg.CreateMap<BillForCreateDto, Bill>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}
