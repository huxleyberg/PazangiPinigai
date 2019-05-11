using System;
using System.Collections.Generic;
using BankAggregator.Domain.Models;

namespace BankAggregator.Core.Services.AccountSummary
{
    public interface IAccountSummaryService
    {
        decimal GetTotalAccountSum(int userId);

        decimal GetTotalIncomeSumForCustomerFrmTrans(int userId);
        decimal GetTotalIncomeSumForAccountFrmTrans(string accountNumber);

        decimal GetTotalExpenseSumForCustomerFrmTrans(int userId);
        decimal GetTotalExpenseSumForAccountFrmTrans(string accountNumber);

        List<accountModel> GetAllCustomerAccounts(int userId);
    }
}
