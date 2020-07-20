using System;

namespace Account.Services.Models
{
    public class AccountModel
    {
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
