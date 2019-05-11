using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;
namespace BankAggregator.Core.Services.SEB
{
    public interface ISEBAccountAuthService
    {
        string AuthRedirectUrl();

        List<BankInformation> GetSEBBanks();

        BankInformation GetBankBysandBoxID(string sandbankIdentification);
    }
}
