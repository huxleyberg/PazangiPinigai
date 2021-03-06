﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAggregator.Domain.Models;
using BankAggregator.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankAggregator.Core.Services.MedBank;
using BankAggregator.Core.Services.Banks;
using BankAggregator.Core.Services.Transactions;
using BankAggregator.Core.Services.AccountSummary;
using BankAggregator.Core.Services.SEB;
using Microsoft.EntityFrameworkCore;
using BankAggregator.Domain.EF;

namespace BankAggregator.Web
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IMedBankServices, MedBankServices>();
            services.AddScoped<IBankService, BankService>();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountSummaryService, AccountSummaryService>();
            services.AddScoped<ISEBAccountAuthService, SEBAccountAuthService>();

            services.AddDbContext<FinAggregatorDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AggregatorContextConnection")));

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
