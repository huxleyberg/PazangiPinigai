using System;
using System.Collections.Generic;

namespace BankAggregator.Core.DTOs.AccountInfo
{
    public class MedBankAccountInfoResponse
    {
        public string resourceId { get; set; }
        public string iban { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public List<Balance> balances { get; set; }
        public Links _links { get; set; }
    }
    public class BalanceAmount
    {
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class Balance
    {
        public BalanceAmount balanceAmount { get; set; }
    }

    public class Balances
    {
        public string href { get; set; }
    }

    public class Transactions
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Balances balances { get; set; }
        public Transactions transactions { get; set; }
    }
}
