using System;
using System.Threading.Tasks;
using BankAggregator.Core.DTOs;

namespace BankAggregator.Core.Services.MedBank
{
    public interface IMedBankServices
    {
        Task<string> GetConsent();

        void GetAllAccounts();

        Task GetBalanceAccountById(string accountId);

        Task<BankInformation> GetAccountInfoById(string accountId);
    }
}
