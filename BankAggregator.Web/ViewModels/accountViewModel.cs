using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAggregator.Web.ViewModels
{
    public class accountViewModel
    {
        public string bankName { get; set; }
        public string acctNumber { get; set; }
        public Decimal? transactionLimit { get; set; }
    }
}
