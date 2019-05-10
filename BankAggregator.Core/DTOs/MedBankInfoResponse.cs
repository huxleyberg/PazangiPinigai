using System;
using System.Collections.Generic;

namespace BankAggregator.Core.DTOs
{
    public class Account
    {
        public string iban { get; set; }
        public string currency { get; set; }
    }

    public class TransactionAmount
    {
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class CreditorAccount
    {
        public string iban { get; set; }
        public string currency { get; set; }
    }

    public class DebtorAccount
    {
        public string iban { get; set; }
        public string currency { get; set; }
    }

    public class Booked
    {
        public string transactionId { get; set; }
        public TransactionAmount transactionAmount { get; set; }
        public string creditorName { get; set; }
        public CreditorAccount creditorAccount { get; set; }
        public string debtorName { get; set; }
        public DebtorAccount debtorAccount { get; set; }
    }

    public class Transactions
    {
        public List<Booked> booked { get; set; }
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


    public class MedBankInfoResponse
    {
        public Account account { get; set; }
        public Transactions transactions { get; set; }
        public List<Balance> balances { get; set; }
    }
}
