using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;
using System.Linq;

namespace BankAggregator.Core.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        public TransactionService()
        {
        }

        public List<Transaction> GetAccountTransactions(string accountnumber)
        {
            List<Transaction> trans = GetAllTransactions();
            trans = trans.Where(x => x.AccountNumber == accountnumber).ToList();

            return trans;
        }

        public List<Transaction> GetAllTransactions()
        {
            //new BankInformation{ AccountBalance = 102.34M, AccountNumber = "", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Jay Barton", SandboxIdentification = "9311219639"},
            //new BankInformation { AccountBalance = 192.34M, AccountNumber = "84386f81a-12a3-4b8a-a13d-de58adf4106d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Francisco Luna", SandboxIdentification = "9311219589" },

            List<Transaction> transactions = new List<Transaction>
            {
                new Transaction{ AccountName= "Some Account name", AccountNumber="ACC_ID_123456789", Amount=123457.78M, Currency="EUR",
                    SourceDestinationBank = "SEB/MED",
                    TransactionRef = "1", TransactionType="C", TransDate = new DateTime(2019,5,5)},
                new Transaction{ AccountName= "Some Account name", AccountNumber="ACC_ID_123456789", Amount=1.0M, Currency="EUR",
                    SourceDestinationBank = "SEB/MED",
                    TransactionRef = "1", TransactionType="D", TransDate = new DateTime(2019,5,5)},

                new Transaction{ AccountName= "Jay Barton", AccountNumber="8386f81a-11a3-4b8a-a10d-de58adf4206d", Amount=103.34M, Currency="EUR",
                    SourceDestinationBank = "MED/MED",
                    TransactionRef = "1", TransactionType="C", TransDate = new DateTime(2019,5,5)},
                new Transaction{ AccountName= "Jay Barton", AccountNumber="8386f81a-11a3-4b8a-a10d-de58adf4206d", Amount=1.0M, Currency="EUR",
                    SourceDestinationBank = "MED/MED",
                    TransactionRef = "1", TransactionType="D", TransDate = new DateTime(2019,5,5)},

                new Transaction{ AccountName= "Francisco Luna", AccountNumber="84386f81a-12a3-4b8a-a13d-de58adf4106d", Amount=193.34M, Currency="EUR",
                    SourceDestinationBank = "MED/MED",
                    TransactionRef = "1", TransactionType="C", TransDate = new DateTime(2019,5,5)},
                new Transaction{ AccountName= "Francisco Luna", AccountNumber="84386f81a-12a3-4b8a-a13d-de58adf4106d", Amount=1.0M, Currency="EUR",
                    SourceDestinationBank = "MED/MED",
                    TransactionRef = "1", TransactionType="D", TransDate = new DateTime(2019,5,5)}


            };

            return transactions;
        }
    }
}
