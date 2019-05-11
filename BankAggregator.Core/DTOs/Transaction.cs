using System;
namespace BankAggregator.Core.DTOs
{
    public class Transaction
    {
        public string TransactionRef { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime TransDate { get; set; }
        public string SourceDestinationBank { get; set; }
    }
}
