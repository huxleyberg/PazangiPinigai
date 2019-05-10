using System;
namespace BankAggregator.Core.Services.MedBank
{
    public class MedBankServices
    {
        private Guid TransactionId;
        public MedBankServices()
        {
            TransactionId = Guid.NewGuid();
        }
    }
}
