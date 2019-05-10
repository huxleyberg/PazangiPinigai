using System;
using RestSharp;

namespace BankAggregator.Core.Services.MedBank
{
    public class MedBankServices : IMedBankServices
    {
        private Guid TransactionId;
        public MedBankServices()
        {
            TransactionId = Guid.NewGuid();
        }

        public string GetAccountById(string accountId)
        {
            throw new NotImplementedException();
        }

        public void GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public string GetConsent()
        {
            string consent = string.Empty;

            string requestId = Guid.NewGuid().ToString();

            var client = new RestClient("https://developers.medbank.lt/psd2/v1/consents");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("X-Request-ID", requestId);
            //request.AddParameter("application/x-www-form-urlencoded", $"client_id=vexruMdCnIFFWu63X64R&client_secret=K3QfdE2nlCAKsGFtUo3Y&code={code}&redirect_uri=https://localhost:5001/home/oauth&grant_type=authorization_code&scope=fiokjub", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return consent;
        }
    }
}
