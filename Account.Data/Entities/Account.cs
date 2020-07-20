using System;

namespace Account.Data.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
