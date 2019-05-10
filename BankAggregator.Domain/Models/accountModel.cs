using System;
using System.Collections.Generic;
using System.Text;

namespace BankAggregator.Domain.Models
{
    public class accountModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public appUser User { get; set; }

        public decimal? TransactionLimit { get; set; }
    }
}
