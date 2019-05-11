using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAggregator.Web.Models;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BankAggregator.Domain.Models;
using BankAggregator.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using BankAggregator.Core.Services.Transactions;
using BankAggregator.Core.Services.AccountSummary;
using BankAggregator.Core.Services.Banks;
using BankAggregator.Core.Services.MedBank;
using BankAggregator.Core.Services.Banks;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankAggregator.Core.Services.SEB;

namespace BankAggregator.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AggregatorContext _context;
        private UserManager<appUser> _userManager;
        private readonly ITransactionService _transactionService;
        private readonly IAccountSummaryService _accountSummaryService;
        private readonly IBankService _bankService;
        private IMedBankServices _medBankServices;
        private readonly ISEBAccountAuthService _sEBAccountAuthService;


        public HomeController(AggregatorContext context, UserManager<appUser> userManager, ITransactionService transactionService,
            IAccountSummaryService accountSummaryService, IBankService bankService, ISEBAccountAuthService sEBAccountAuthService, IMedBankServices medBankServices)
        {
            _context = context;
            _userManager = userManager;
            _transactionService = transactionService;
            _accountSummaryService = accountSummaryService;
            _bankService = bankService;
            _sEBAccountAuthService = sEBAccountAuthService;
            _medBankServices = medBankServices;

        }
        //public HomeController(AggregatorContext context, UserManager<appUser> userManager, IMedBankServices medBankServices)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //    _medBankServices = medBankServices;
        //}
        public IActionResult Index()
        {
            var client = new RestClient("https://api-sandbox.sebgroup.com/mga/sps/oauth/oauth20/authorize?client_id=vexruMdCnIFFWu63X64R&scope=accounts&redirect_uri=https://localhost:5001/home/oauth&response_type=code");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "text/html");
            IRestResponse response = client.Execute(request);

            //return Redirect(response.ResponseUri.AbsoluteUri);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = "";
            if (user == null)
            {
                userId = "1";
            }
            else
            {
                userId = user.Id;
            }


            DashboardVM model = new DashboardVM();
            model.CustomerAccounts = _accountSummaryService.GetAllCustomerAccounts(userId);
            model.CustomerTransactions = new List<Core.DTOs.Transaction>();

            foreach (var item in model.CustomerAccounts)
            {
                var trans = _transactionService.GetAccountTransactions(item.BankAccountNumber);
                foreach (var tran in trans)
                {
                    model.CustomerTransactions.Add(tran);
                }
            }

            model.TotalAccountCount = _accountSummaryService.TotalAccountCount(userId);
            model.TotalAccountSum = _accountSummaryService.GetTotalAccountSum(userId);
            model.TotalExpenses = _accountSummaryService.GetTotalExpenseSumForCustomerFrmTrans(userId);
            model.TotalExpenseTransactionsCount = _accountSummaryService.TotalExpenseTransCount(userId);
            model.TotalIncomeSum = _accountSummaryService.GetTotalIncomeSumForCustomerFrmTrans(userId);
            model.TotalIncomeTransactionsCount = _accountSummaryService.TotalIncomeTransCount(userId);
            model.TransLimit = _accountSummaryService.TransLimit(userId);


            return View(model);
        }



        public async Task<IActionResult> oauth(string code)
        {
            var acctModel = new accountModel();

            var bankInfo = _sEBAccountAuthService.GetBankBysandBoxID("9311219639");

            acctModel.BankName = "SEB";
            acctModel.BankAccountNumber = bankInfo.AccountNumber;
            acctModel.BankAccountName = bankInfo.AccountName;
            acctModel.Currency = bankInfo.Currency;
            acctModel.AccountType = bankInfo.AccountType;
            acctModel.Balance = bankInfo.AccountBalance;
            acctModel.TotalExpense = bankInfo.TotalExpenses;
            acctModel.TotalIncome = bankInfo.TotalIncome;
            acctModel.SandboxIdentification = bankInfo.SandboxIdentification;
            acctModel.CreatedAt = DateTime.Now;
            acctModel.User = await _userManager.GetUserAsync(HttpContext.User);
            acctModel.TransactionLimit = 15.0M;

            _context.AccountModels.Add(acctModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult addBank()
        {
            var Banks = new BankService();
            ViewData["BankList"] = new SelectList(Banks.GetBanks(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addBank(accountViewModel model)
        {
            var acctModel = new accountModel();
            //string accountId = "ACC _ID_" + model.acctNumber; 

            if (model.bankName.ToLower() == "seb")
            {
                TempData["accountnumber"] = model.acctNumber;
                var redirecturl = _sEBAccountAuthService.AuthRedirectUrl();
                return Redirect(redirecturl);
            }
            else
            {

                var bankInfo = _medBankServices.GetAccountInfoById(model.acctNumber).Result;
                acctModel.BankName = model.bankName;
                acctModel.BankAccountNumber = bankInfo.AccountNumber;
                acctModel.BankAccountName = bankInfo.AccountName;
                acctModel.Currency = bankInfo.Currency;
                acctModel.AccountType = bankInfo.AccountType;
                acctModel.Balance = bankInfo.AccountBalance;
                acctModel.TotalExpense = bankInfo.TotalExpenses;
                acctModel.TotalIncome = bankInfo.TotalIncome;
                acctModel.SandboxIdentification = bankInfo.SandboxIdentification;
                acctModel.CreatedAt = DateTime.Now;
                acctModel.User = await _userManager.GetUserAsync(HttpContext.User);
                acctModel.TransactionLimit = model.transactionLimit;

                _context.AccountModels.Add(acctModel);
                await _context.SaveChangesAsync();
            }



            return RedirectToAction(nameof(Dashboard));
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
