using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;

namespace BankAggregator.Core.Services.Banks
{
    public interface IBankService
    {
        List<Bank> GetBanks();
    }
}
