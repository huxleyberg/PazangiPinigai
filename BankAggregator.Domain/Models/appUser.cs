using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAggregator.Domain.Models
{
    public class appUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ICollection<accountModel> Accounts { get; set; }
    }
}
