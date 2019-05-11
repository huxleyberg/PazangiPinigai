using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;

namespace BankAggregator.Core.Services.Banks
{
    public class BankService : IBankService
    {
        public List<Bank> GetBanks()
        {
            List<Bank> banks = new List<Bank>
            {
                new Bank{Id = 1, Name="SEB"},
                new Bank{Id = 2, Name="MEDICINOUS BANKAS"}
            };

            return banks;
        }
    }
}
