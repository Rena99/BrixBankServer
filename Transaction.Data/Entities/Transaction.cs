using System;

namespace Transaction.Data.Entities
{
    public enum Status
    {
        Processing,
        Success,
        Fail
    }
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public string FailureReason { get; set; }
    }
}
