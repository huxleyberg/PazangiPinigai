using System;
using System.Collections.Generic;
using BankAggregator.Domain.Models;
using BankAggregator.Core.Services.Transactions;
using BankAggregator.Core.DTOs;
using System.Linq;
using BankAggregator.Domain.EF;

namespace BankAggregator.Core.Services.AccountSummary
{
    public class AccountSummaryService : IAccountSummaryService
    {
        private readonly ITransactionService _transactionService;
        private FinAggregatorDbContext _context;

        public AccountSummaryService(ITransactionService transactionService, FinAggregatorDbContext context)
        {
            _transactionService = transactionService;
            _context = context;
        }

        public List<accountModel> GetAllCustomerAccounts(string userId)
        {
            var accounts = _context.accountModels.Where(x => x.appUserId == userId).ToList();
            return accounts;
        }

        public decimal GetTotalAccountSum(string userId)
        {
            decimal total = 0M;
            var accts = GetAllCustomerAccounts(userId);
            if (accts != null)
            {
                total = accts.Sum(x => (decimal)x.Balance);
            }

            return total;
        }

        public decimal GetTotalExpenseSumForAccountFrmTrans(string accountNumber)
        {
            decimal sum = 0M;
            List<Transaction> trans = _transactionService.GetAccountTransactions(accountNumber);
            trans = trans.Where(x => x.TransactionType.ToLower() == "d").ToList();

            if (trans != null)
            {
                sum = trans.Sum(x => x.Amount);
            }
            return sum;
        }

        public decimal GetTotalExpenseSumForCustomerFrmTrans(string userId)
        {
            //get the accounts belonging to the user.
            var accts = GetAllCustomerAccounts(userId);

            var expenseTotal = 0M;

            foreach (var acct in accts)
            {
                var trans = _transactionService.GetAccountTransactions(acct.BankAccountNumber);
                var total = 0M;
                if (trans != null)
                {
                    trans = trans.Where(x => x.TransactionType.ToLower() == "d").ToList();
                    total = trans.Sum(x => x.Amount);

                    expenseTotal = expenseTotal + total;
                }
            }
            return expenseTotal;
        }

        public decimal GetTotalIncomeSumForAccountFrmTrans(string accountNumber)
        {
            decimal sum = 0M;
            List<Transaction> trans = _transactionService.GetAccountTransactions(accountNumber);
            trans = trans.Where(x => x.TransactionType.ToLower() == "c").ToList();

            if (trans != null)
            {
                sum = trans.Sum(x => x.Amount);
            }
            return sum;
        }

        public decimal GetTotalIncomeSumForCustomerFrmTrans(string userId)
        {

            //get the accounts belonging to the user.
            var accts = GetAllCustomerAccounts(userId);

            var incomeTotal = 0M;

            foreach (var acct in accts)
            {
                var trans = _transactionService.GetAccountTransactions(acct.BankAccountNumber);
                var total = 0M;
                if (trans != null)
                {
                    trans = trans.Where(x => x.TransactionType.ToLower() == "c").ToList();
                    total = trans.Sum(x => x.Amount);

                    incomeTotal = incomeTotal + total;
                }
            }
            return incomeTotal;
        }

        public int TotalAccountCount(string userId)
        {
            return _context.accountModels.Where(x => x.appUserId == userId).Count();
        }

        public int TotalExpenseTransCount(string userId)
        {
            //get the accounts belonging to the user.
            var accts = GetAllCustomerAccounts(userId);

            var expenseTotal = 0;

            foreach (var acct in accts)
            {
                var trans = _transactionService.GetAccountTransactions(acct.BankAccountNumber);
                var total = 0;
                if (trans != null)
                {
                    trans = trans.Where(x => x.TransactionType.ToLower() == "d").ToList();
                    total = trans.Count;

                    expenseTotal = expenseTotal + total;
                }
            }
            return expenseTotal;
        }

        public int TotalIncomeTransCount(string userId)
        {
            //get the accounts belonging to the user.
            var accts = GetAllCustomerAccounts(userId);

            var incomeTotal = 0;

            foreach (var acct in accts)
            {
                var trans = _transactionService.GetAccountTransactions(acct.BankAccountNumber);
                var total = 0;
                if (trans != null)
                {
                    trans = trans.Where(x => x.TransactionType.ToLower() == "c").ToList();
                    total = trans.Count;

                    incomeTotal = incomeTotal + total;
                }
            }
            return incomeTotal;
        }

        public decimal TransLimit(string userId)
        {
            decimal total = 0M;
            var accts = GetAllCustomerAccounts(userId);
            if (accts != null)
            {
                total = accts.Sum(x => (decimal)x.TransactionLimit);
            }

            return total;
        }


    }
}
