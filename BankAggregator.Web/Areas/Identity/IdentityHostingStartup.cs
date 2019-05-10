using System;
using BankAggregator.Domain.Models;
using BankAggregator.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BankAggregator.Web.Areas.Identity.IdentityHostingStartup))]
namespace BankAggregator.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AggregatorContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AggregatorContextConnection")));

                services.AddDefaultIdentity<appUser>()
                    .AddEntityFrameworkStores<AggregatorContext>();
            });
        }
    }
}