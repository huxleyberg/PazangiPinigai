using System;
using System.Collections.Generic;
using BankAggregator.Domain.Models;
using BankAggregator.Core.DTOs;
namespace BankAggregator.Web.ViewModels
{
    public class DashboardVM
    {
        public decimal TotalAccountSum { get; set; }
        public decimal TotalIncomeSum { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<accountModel> CustomerAccounts { get; set; }
        public List<Transaction> CustomerTransactions { get; set; }
        public int TotalAccountCount { get; set; }
        public int TotalIncomeTransactionsCount { get; set; }
        public int TotalExpenseTransactionsCount { get; set; }
        public decimal TransLimit { get; set; }
    }
}
