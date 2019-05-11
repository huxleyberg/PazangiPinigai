using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;
namespace BankAggregator.Core.Services.Transactions
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        List<Transaction> GetAccountTransactions(string accountnumber);
    }
}
