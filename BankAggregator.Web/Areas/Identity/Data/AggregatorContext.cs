using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAggregator.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankAggregator.Web.Models
{
    public class AggregatorContext : IdentityDbContext<appUser>
    {
        public AggregatorContext(DbContextOptions<AggregatorContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<appUser> appUsers { get; set; }
        public virtual DbSet<accountModel> AccountModels { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
