using System;
using System.Collections.Generic;
using System.Text;

namespace BankAggregator.Domain.Models
{
    public class accountModel
    {
        public int Id { get; set; }
        public string appUserId { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public decimal? Balance { get; set; }
        public string Currency { get; set; }
        public DateTime? CreatedAt { get; set; }
        public appUser User { get; set; }

        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }

        public decimal? TransactionLimit { get; set; }
        public string AccountType { get; set; }
        public string SandboxIdentification { get; set; }
    }
}
