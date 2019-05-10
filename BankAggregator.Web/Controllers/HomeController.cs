﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAggregator.Web.Models;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace BankAggregator.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var client = new RestClient("https://api-sandbox.sebgroup.com/mga/sps/oauth/oauth20/authorize?client_id=vexruMdCnIFFWu63X64R&scope=accounts&redirect_uri=https://localhost:5001/home/oauth&response_type=code");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Accept", "text/html");
            //IRestResponse response = client.Execute(request);
            //return Redirect(response.ResponseUri.AbsoluteUri);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult oauth(string code)
        {
            ViewData["Message"] = $"{code}";
            var client = new RestClient("https://api-sandbox.sebgroup.com/mga/sps/oauth/oauth20/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"client_id=vexruMdCnIFFWu63X64R&client_secret=K3QfdE2nlCAKsGFtUo3Y&code={code}&redirect_uri=https://localhost:5001/home/oauth&grant_type=authorization_code&scope=fiokjub", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Dictionary<string, string> tokenInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            string token = tokenInfo["access_token"];
            string refresh_token = tokenInfo["refresh_token"];

            ViewData["token"] = token;
            ViewData["refresh_token"] = refresh_token;

            string requestId = Guid.NewGuid().ToString();

            var client1 = new RestClient("https://api-sandbox.sebgroup.com/ais/v4/identified2/accounts?withBalance=false");
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Accept", "application/json");
            //request1.AddHeader("psu-http-method", "GET");
            //request1.AddHeader("psu-corporate-id", "40073144970009");
            request1.AddHeader("authorization", $"Bearer {token}");
            request1.AddHeader("x-request-id", requestId);
            IRestResponse response1 = client1.Execute(request1);

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            string requestId = Guid.NewGuid().ToString();

            var client = new RestClient("https://api-sandbox.sebgroup.com/ais/v4/identified2/accounts?withBalance=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("psu-http-method", "GET");
            //request.AddHeader("psu-corporate-id", "40073144970009");
            request.AddHeader("authorization", "bearer 4CEWSXXvSRY1b1irTVWY");
            request.AddHeader("x-request-id", requestId);
            IRestResponse response = client.Execute(request);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
