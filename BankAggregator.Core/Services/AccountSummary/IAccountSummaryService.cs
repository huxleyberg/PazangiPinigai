using System;
using System.Collections.Generic;
using BankAggregator.Domain.Models;

namespace BankAggregator.Core.Services.AccountSummary
{
    public interface IAccountSummaryService
    {
        decimal GetTotalAccountSum(string userId);

        int TotalAccountCount(string userId);
        decimal TransLimit(string userId);
        int TotalIncomeTransCount(string userId);
        int TotalExpenseTransCount(string userId);

        decimal GetTotalIncomeSumForCustomerFrmTrans(string userId);
        decimal GetTotalIncomeSumForAccountFrmTrans(string accountNumber);

        decimal GetTotalExpenseSumForCustomerFrmTrans(string userId);
        decimal GetTotalExpenseSumForAccountFrmTrans(string accountNumber);

        List<accountModel> GetAllCustomerAccounts(string userId);
    }
}
