using System;

namespace Transaction.Services.Models
{
    public class TransactionModel
    {
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string FailureReason { get; set; }
    }
}
