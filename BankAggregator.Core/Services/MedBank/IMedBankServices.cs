using System;
namespace BankAggregator.Core.Services.MedBank
{
    public interface IMedBankServices
    {
        string GetConsent();

        void GetAllAccounts();

        string GetAccountById(string accountId);
    }
}
