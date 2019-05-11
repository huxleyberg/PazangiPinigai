using RestSharp;
using System;

namespace testApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api-sandbox.sebgroup.com/mga/sps/oauth/oauth20/authorize?client_id=vexruMdCnIFFWu63X64R&scope=psd2_accounts&redirect_uri=https://localhost:5001/home/oauth&response_type=REPLACE_THIS_VALUE&state=REPLACE_THIS_VALUE");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "text/html");
            IRestResponse response = client.Execute(request);



            Console.WriteLine("Hello World!");
        }
    }
}
