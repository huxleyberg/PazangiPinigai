using System;
namespace BankAggregator.Core.DTOs
{
    public class BankInformation
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
