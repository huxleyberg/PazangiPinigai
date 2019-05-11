using System;
using System.Collections.Generic;
using BankAggregator.Core.DTOs;
using RestSharp;
using System.Linq;

namespace BankAggregator.Core.Services.SEB
{
    public class SEBAccountAuthService : ISEBAccountAuthService
    {
        public string AuthRedirectUrl()
        {
            var client = new RestClient("https://api-sandbox.sebgroup.com/mga/sps/oauth/oauth20/authorize?client_id=vexruMdCnIFFWu63X64R&scope=accounts&redirect_uri=https://localhost:5001/home/oauth&response_type=code");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "text/html");
            IRestResponse response = client.Execute(request);
            return response.ResponseUri.AbsoluteUri;
        }

        public BankInformation GetBankBysandBoxID(string sandbankIdentification)
        {
            BankInformation bank = new BankInformation();
            var banks = GetSEBBanks();

            bank = banks.FirstOrDefault(x => x.SandboxIdentification == sandbankIdentification);

            return bank;
        }

        public List<BankInformation> GetSEBBanks()
        {
            List<BankInformation> banks = new List<BankInformation>
            {
                new BankInformation{ AccountBalance = 102.34M, AccountNumber = "8386f81a-11a3-4b8a-a10d-de58adf4206d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Jay Barton", SandboxIdentification = "9311219639"},
                new BankInformation{ AccountBalance = 192.34M, AccountNumber = "84386f81a-12a3-4b8a-a13d-de58adf4106d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Francisco Luna", SandboxIdentification = "9311219589"},
                new BankInformation{ AccountBalance = 112.34M, AccountNumber = "4386f81a-13a3-4b8a-a10d-de58adf4806d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Rosa Armstrong", SandboxIdentification = "8811215477"},
                new BankInformation{ AccountBalance = 152.34M, AccountNumber = "5386f81a-14a3-4b8a-a17d-de58adf4806d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Alex Fletcher", SandboxIdentification = "8811212862"},
                new BankInformation{ AccountBalance = 12.34M, AccountNumber = "1386f81a-15a3-4b8a-a19d-de58adf4906d", AccountType = "Privatkonto", Currency = "SEK", AccountName = "Lönekonto", SandboxIdentification = "8311211356"}
            };

            return banks;
        }
    }
}
