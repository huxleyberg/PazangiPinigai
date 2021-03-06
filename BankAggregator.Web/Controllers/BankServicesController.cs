﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using BankAggregator.Core.Services.MedBank;

namespace BankAggregator.Web.Controllers
{
    public class BankServicesController : Controller
    {
        private readonly IMedBankServices _medBankServices;

        public BankServicesController(IMedBankServices medBankServices)
        {
            _medBankServices = medBankServices;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var consent = _medBankServices.GetConsent();

            var bankInfo = _medBankServices.GetAccountInfoById("ACC_ID_123456789").Result;

            return View();
        }
    }
}
