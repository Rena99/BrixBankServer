using System;

namespace Transaction.WebApi.DTO
{
    public class TransactionDTO
    {
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public int Amount { get; set; }
    }
}
