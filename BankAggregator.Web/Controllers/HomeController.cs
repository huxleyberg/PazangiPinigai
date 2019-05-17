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
using Microsoft.Extensions.Logging;
using BankAggregator.Web.Areas.Identity.Pages.Account;
using System.ComponentModel.DataAnnotations;
using LoginModel = BankAggregator.Web.ViewModels.LoginModel;
using System.Text.Encodings.Web;

namespace BankAggregator.Web.Controllers
{   
   
    public class HomeController : Controller
    {
        private AggregatorContext _context;
        private UserManager<appUser> _userManager;
        private readonly ITransactionService _transactionService;
        private readonly IAccountSummaryService _accountSummaryService;
        private readonly IBankService _bankService;
        private IMedBankServices _medBankServices;
        private readonly ISEBAccountAuthService _sEBAccountAuthService;
        private readonly SignInManager<appUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;


        public HomeController(AggregatorContext context, UserManager<appUser> userManager, ITransactionService transactionService,
            IAccountSummaryService accountSummaryService, IBankService bankService, SignInManager<appUser> signInManager, ILogger<LoginModel> logger, ISEBAccountAuthService sEBAccountAuthService, IMedBankServices medBankServices)
        {
            _context = context;
            _userManager = userManager;
            _transactionService = transactionService;
            _accountSummaryService = accountSummaryService;
            _bankService = bankService;
            _sEBAccountAuthService = sEBAccountAuthService;
            _medBankServices = medBankServices;
            _signInManager = signInManager;
            _logger = logger;

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

        public async Task<IActionResult> login(ProfileModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.LoginModel.Email, Input.LoginModel.Password, Input.LoginModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //get the object via the email
                    return RedirectToAction(nameof(Dashboard));
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.LoginModel.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction(nameof(Index));
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> register(ViewModels.ProfileModel Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new appUser { UserName = Input.RegisterModel.Email, Email = Input.RegisterModel.Email, PhoneNumber = Input.RegisterModel.PhoneNumber, FullName = Input.RegisterModel.FullName, CreatedAt = DateTime.Now };
                var result = await _userManager.CreateAsync(user, Input.RegisterModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Dashboard));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
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
            ViewData["BankList"] = new SelectList(Banks.GetBanks(), "Name", "Name");
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
