using System;
using System.Collections.Generic;

namespace BankAggregator.Core.DTOs
{
    public class ConsentRequest
    {
        public Access access { get; set; }
        public bool recurringIndicator { get; set; }
        public string validUntil { get; set; }
        public int frequencyPerDay { get; set; }
        public bool combinedServiceIndicator { get; set; }
    }

    public class Access
    {
        public List<object> accounts { get; set; }
    }
}
