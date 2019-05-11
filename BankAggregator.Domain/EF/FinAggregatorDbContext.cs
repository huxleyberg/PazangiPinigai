using System;
using BankAggregator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAggregator.Domain.EF
{
    public class FinAggregatorDbContext : DbContext
    {
        public FinAggregatorDbContext(DbContextOptions<FinAggregatorDbContext> options) : base(options)
        {
        }

        public DbSet<accountModel> accountModels { get; set; }
    }
}


