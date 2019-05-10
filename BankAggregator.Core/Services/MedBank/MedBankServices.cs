using System;
//using RestSharp;
using BankAggregator.Core.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BankAggregator.Core.DTOs.AccountInfo;

namespace BankAggregator.Core.Services.MedBank
{
    public class MedBankServices : IMedBankServices
    {
        private Guid TransactionId;
        public MedBankServices()
        {
            TransactionId = Guid.NewGuid();
        }

        public async Task GetBalanceAccountById(string accountId)
        {
            HttpClient client = new HttpClient();
            string requestId = Guid.NewGuid().ToString();
            string consentId = await GetConsent();
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("X-Request-ID", requestId);
            client.DefaultRequestHeaders.Add("Consent-ID", consentId);

            //curl -X GET "https://developers.medbank.lt/psd2/v1/accounts/ACC_ID_123456789/transactions?bookingStatus=booked&deltaList=true&withBalance=true" 
            //-H "accept: text/plain" -H "X-Request-ID: 468f1243-6079-4d46-bd41-12b8ed5a8e16" -H "Consent-ID: C_ID_123456789"



            var response = await client.GetAsync(new Uri($"https://developers.medbank.lt/psd2/v1/accounts/{accountId}/transactions?bookingStatus=booked&deltaList=true&withBalance=true"));
            string content = await response.Content.ReadAsStringAsync();
            MedBankInfoResponse result = JsonConvert.DeserializeObject<MedBankInfoResponse>(content);

            //https://developers.medbank.lt/psd2/v1/accounts/ACC_ID_123456789/transactions?bookingStatus=booked&deltaList=true&withBalance=true
        }

        public void GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetConsent()
        {
            HttpClient client = new HttpClient();
            string requestId = Guid.NewGuid().ToString();
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("X-Request-ID", requestId);
            client.DefaultRequestHeaders.Add("PSU-Geo-Location", "dolski.udeogu@gmail.com");

            var obj = new ConsentRequest();
            obj.access = new Access();
            obj.combinedServiceIndicator = false;
            obj.validUntil = "2021-01-01";
            obj.frequencyPerDay = 1;
            obj.combinedServiceIndicator = false;
            var payload = GetPayLoad(obj);

            var response = await client.PostAsync(new Uri("https://developers.medbank.lt/psd2/v1/consents"), payload);
            string content = await response.Content.ReadAsStringAsync();


            ConsentResponse result = JsonConvert.DeserializeObject<ConsentResponse>(content);

            return result.consentId;

        }

        private static StringContent GetPayLoad(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");


        }

        public async Task<BankInformation> GetAccountInfoById(string accountId)
        {
            HttpClient client = new HttpClient();
            string requestId = Guid.NewGuid().ToString();
            string consentId = await GetConsent();
            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("X-Request-ID", requestId);
            client.DefaultRequestHeaders.Add("Consent-ID", consentId);

            var response = await client.GetAsync(new Uri($"https://developers.medbank.lt/psd2/v1/accounts/{accountId}"));
            string content = await response.Content.ReadAsStringAsync();
            MedBankAccountInfoResponse result = JsonConvert.DeserializeObject<MedBankAccountInfoResponse>(content);

            BankInformation info = new BankInformation();
            info.AccountBalance = Convert.ToDecimal(result.balances[0].balanceAmount);
            info.AccountName = result.name;
            info.AccountNumber = accountId;
            info.Currency = result.currency;

            return info;
        }

        //public string GetConsent()
        //{
        //    string consent = string.Empty;

        //    string requestId = Guid.NewGuid().ToString();

        //    var client = new RestClient("https://developers.medbank.lt/psd2/v1/consents");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("X-Request-ID", requestId);
        //    request.AddHeader("PSU-Geo-Location", "dolski.udeogu@gmail.com");

        //    var patchResponse = await client.PostAsync()
        //   // request.AddParameter("accept", '');

        //    request.AddJsonBody(new ConsentRequest());

        //    //request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(yourObject), ParameterType.RequestBody);

        //    //PSU-Geo-Location
        //    //request.AddParameter("application/x-www-form-urlencoded", $"client_id=vexruMdCnIFFWu63X64R&client_secret=K3QfdE2nlCAKsGFtUo3Y&code={code}&redirect_uri=https://localhost:5001/home/oauth&grant_type=authorization_code&scope=fiokjub", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);

        //    return consent;
        //}
    }
}
