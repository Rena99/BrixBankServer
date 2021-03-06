﻿using System;

namespace Account.WebApi.DTO
{
    public class OperationHistoryDTO
    {
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public bool Debit { get; set; }
        public int TransactionAmount { get; set; }
        public int Balance { get; set; }
        public DateTime OperationTime { get; set; }
        public bool Succeeded { get; set; }
    }
}
