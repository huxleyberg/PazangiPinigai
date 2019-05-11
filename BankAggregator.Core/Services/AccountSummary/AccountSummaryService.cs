using System;
using System.Collections.Generic;
using BankAggregator.Domain.Models;

namespace BankAggregator.Core.Services.AccountSummary
{
    public class AccountSummaryService : IAccountSummaryService
    {
        public AccountSummaryService()
        {
        }

        public List<accountModel> GetAllCustomerAccounts(int userId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalAccountSum(int userId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalExpenseSumForAccountFrmTrans(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalExpenseSumForCustomerFrmTrans(int userId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalIncomeSumForAccountFrmTrans(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalIncomeSumForCustomerFrmTrans(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
