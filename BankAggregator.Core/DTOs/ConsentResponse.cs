using System;
namespace BankAggregator.Core.DTOs
{
    public class ConsentResponse
    {
        public string consentStatus { get; set; }
        public string consentId { get; set; }
        public Links _links { get; set; }
    }
    public class StartAuthorisationWithPsuAuthentication
    {
        public string href { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public StartAuthorisationWithPsuAuthentication startAuthorisationWithPsuAuthentication { get; set; }
        public Self self { get; set; }
    }
}
