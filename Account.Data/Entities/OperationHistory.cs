using System;

namespace Account.Data.Entities
{
    public class OperationHistory
    {
        public Guid OperationHistoryId { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public bool Debit { get; set; }
        public int TransactionAmount { get; set; }
        public int Balance { get; set; }
        public DateTime OperationTime { get; set; }
    }
}
