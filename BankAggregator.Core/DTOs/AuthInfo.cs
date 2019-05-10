using System;
namespace BankAggregator.Core.DTOs
{
    public class AuthInfo
    {
        public string token { get; set; }
        public string refresh_token { get; set; }
    }
}
